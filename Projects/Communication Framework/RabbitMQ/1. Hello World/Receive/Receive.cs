using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// creates a new connection factory to connect to RabbitMQ
var factory = new ConnectionFactory { HostName = "localhost" };

// creates a connection to the RabbitMQ server using the connection factory.
using var connection = factory.CreateConnection();

// This creates a channel, which is a virtual connection inside the RabbitMQ connection. Channels are used to send and receive messages.
using var channel = connection.CreateModel();

// This declares a queue named "hello".
channel.QueueDeclare(queue: "hello",
                     durable: false, // The queue will not survive a server restart.
                     exclusive: false, // The queue can be used by other connections.
                     autoDelete: false, // The queue will not be deleted when the last consumer unsubscribes.
                     arguments: null); // No additional arguments are provided.

Console.WriteLine(" [*] Waiting for messages.");

// This creates a new consumer that will handle incoming messages.
var consumer = new EventingBasicConsumer(channel);

// event handler for when a message is received.
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
};

// This starts consuming messages from the "hello" queue.
channel.BasicConsume(queue: "hello",
                     autoAck: true, // the messages are automatically acknowledged as soon as they are received.
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();