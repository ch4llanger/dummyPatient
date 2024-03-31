using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonProject
{
    public class Producer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public Producer(string address, string username, string password)
        {
            var factory = new ConnectionFactory() { HostName = address, UserName = username, Password = password };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void PublishMessage<T>(T message, string exchangeName)
        {
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);

            var serializedMessage = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(serializedMessage);

            _channel.BasicPublish(exchange: exchangeName,
                                  routingKey: string.Empty,
                                  basicProperties: null,
                                  body: body);
        }

        public void Close()
        {
            _connection.Close();
        }
    }
}
