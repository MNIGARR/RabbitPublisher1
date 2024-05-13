using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitConsumer1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://hkqqhlch:b5wm2yEAKLpohoWau42wJvRQNWOBORWt@chimpanzee.rmq.cloudamqp.com/hkqqhlch");
            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                exchange: "logs",
                routingKey: string.Empty);

            var consumer = new EventingBasicConsumer(channel);  
            channel.BasicConsume(queueName, true, consumer);
            consumer.Received += Consumer_Received;

            Console.ReadLine();

        }


        private static void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine($"Received message : {message}");

        }
    }
}
