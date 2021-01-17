using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
namespace RabbitMqTutorial
{
    class Send
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PUBLISHER");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    
                    
                    for(int i = 0; i<100; i++)
                    {
                        string message = "Hello World " + i;
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

                        Console.WriteLine("Sent {0}", message);
                    }
                    
                }
            }

            Console.WriteLine("Press a key to exit.");
            Console.ReadLine();
        }
    }
}
