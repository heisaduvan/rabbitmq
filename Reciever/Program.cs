using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace Reciever
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RECEIVER");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var conn = factory.CreateConnection())
            {
                using(var channel = conn.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false,autoDelete:false,arguments:null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received {0}", message);
                    };
                    channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
                    Console.WriteLine("Press a key to exit.");
                    Console.ReadLine();
                }

            }
        }
    }
}
