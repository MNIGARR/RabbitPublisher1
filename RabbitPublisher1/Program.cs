using RabbitMQ.Client;
using System.Text;

namespace RabbitPublisher1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://psqyqkas:QYmaSp0TjmkU91ADQ-m0oc8AZFob-vhj@puffin.rmq2.cloudamqp.com/psqyqkas");
            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "dirext_logs", type: ExchangeType.Direct);
            var message = "Hello Worl";
            while (true)
            {
                Console.WriteLine("Warning or Error or Info");
                var severity = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange:"direct_logs",
                    routingKey: severity,
                    basicProperties: null
               
            }

        }
    }
}
