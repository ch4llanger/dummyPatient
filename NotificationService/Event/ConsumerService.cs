using AppointmentAPI.Events;
using CommonProject;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotificationAPI.Domain;
using NotificationService.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NotificationAPI.Events
{
    public class ConsumerService: IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IConnection _connection;
        private IModel _channel;
        private readonly Settings _settings;

        public ConsumerService(IServiceScopeFactory serviceScopeFactory, IOptions<Settings> settings)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _settings = settings.Value;
        }

        public async Task PersistEventMessage(byte[] body, string exchangeName)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<NotificationServiceContext>();

            string message = Encoding.UTF8.GetString(body);

            var entity = JsonConvert.DeserializeObject<AppointmentEvent>(message);

            var newNotification = Notification.Create(entity.PatientId, $"{entity.DepartmentName} departmanına {entity.Date} tarihli randevuzun oluşturulmuştur.");

            context.Notification.Add(newNotification);

            await context.SaveChangesAsync();

            string subject = "Randevunuz Hazırlandı!";

            string messageBody = $"{entity.DepartmentName} departmanına {entity.Date} tarihli randevuzun oluşturulmuştur..";

            Console.WriteLine(subject);
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

            _channel.ExchangeDeclare(exchange: "appointment", type: ExchangeType.Fanout);
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: "appointment", routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) => await PersistEventMessage(ea.Body.ToArray(), ea.Exchange);

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

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
