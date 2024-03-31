
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using CommonProject;
using DiscussionAPI.Domain;
using QuestionService.Data;

namespace QuestionService.Event
{
    public class ConsumerService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Settings _settings;
        private IConnection _connection;
        private IModel _channel;

        public ConsumerService(IServiceScopeFactory serviceScopeFactory, IOptions<Settings> settings)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _settings = settings.Value;
        }

        public async Task PersistEventMessage(byte[] body, string exchangeName)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<QuestionServiceContext>();

            string message = Encoding.UTF8.GetString(body);

            if (exchangeName == "patient")
            {
                var entity = JsonConvert.DeserializeObject<Patient>(message);

                var patient = await context.Patients.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (patient is not null)
                {
                    patient.Update( entity.Name, entity.Surname);
                }
                else
                {
                    context.Add(entity);
                }
            }
            else if (exchangeName == "doctor")
            {
                var entity = JsonConvert.DeserializeObject<Doctor>(message);

                var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (doctor is not null)
                {
                    doctor.Update(entity.Name, entity.Surname);
                }
                else
                {
                    context.Add(entity);
                }

            }

            await context.SaveChangesAsync();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.ConnectionAddress,
                UserName = _settings.Username,
                Password = _settings.Password,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "patient", type: ExchangeType.Fanout);
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: "patient", routingKey: "");

            _channel.ExchangeDeclare(exchange: "doctor", type: ExchangeType.Fanout);
            var doctorQueueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: doctorQueueName, exchange: "doctor", routingKey: "");

            _channel.ExchangeDeclare(exchange: "department", type: ExchangeType.Fanout);
            var departmentQueueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: departmentQueueName, exchange: "department", routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) => await PersistEventMessage(ea.Body.ToArray(), ea.Exchange);

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            _channel.BasicConsume(queue: doctorQueueName, autoAck: true, consumer: consumer);
            _channel.BasicConsume(queue: departmentQueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
