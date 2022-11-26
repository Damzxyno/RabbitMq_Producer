using RabbitMQ.Client;

namespace Sender;
class Send
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using (var connection = factory.CreateConnection()) 
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(
                queue: "Hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
            for (var i = 1; i <=10; i++)
            {
                var message = Encoding.UTF8.GetBytes($"Sending message {i} now");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                channel.BasicPublish(
                exchange: "",
                routingKey: "Hello",
                basicProperties: null,
                body: message
                );
            }

            

            Console.WriteLine("Done sending message");
            Console.ReadLine();
        }
    }
}