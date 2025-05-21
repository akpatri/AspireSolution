
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ProjectC.Services
{
    public class WorkerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public WorkerService(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
        }

        int i = 0;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel.QueueDeclare(
                 queue: "test_queue",
                 durable: false,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null);
            _channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}:{i++}");
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(queue: "test_queue",
                                 autoAck: false,
                                 consumer: consumer);
            return Task.CompletedTask;
        }
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }

}