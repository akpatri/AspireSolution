using RabbitMQ.Client;
using System.Text;

namespace ProjectB.Services
{
    public class MessageService
    {
        private IConnection _connection;
        
        public MessageService(IConnection connection)
        {
            _connection = connection;   
        }
        public void SendMessage(string msg) { 
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: "test_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var message = msg;
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "",
                                 routingKey: "test_queue",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent {message}");

        }

        
    }
}
