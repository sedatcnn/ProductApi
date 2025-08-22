using CaseStudy.Domian.Interfaces;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace CaseStudy.Persistence.Repositories
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQEventBus()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "product_events", type: ExchangeType.Fanout);
        }

        public void Publish<T>(T eventData)
        {
            if (_channel == null || _connection == null || !_connection.IsOpen)
            {
                return;
            }

            var message = JsonSerializer.Serialize(eventData);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
              exchange: "product_events",
              routingKey: "",
              basicProperties: null,
              body: body
            );
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}