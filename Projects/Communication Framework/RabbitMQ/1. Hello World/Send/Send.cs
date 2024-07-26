using System.Text;
using RabbitMQ.Client;

// This creates a new connection factory to connect to RabbitMQ.
var factory = new ConnectionFactory { HostName = "localhost" };

// This creates a connection to the RabbitMQ server using the connection factory.
using var connection = factory.CreateConnection();

// This creates a channel, which is a virtual connection inside the RabbitMQ connection. Channels are used to send and receive messages.
using var channel = connection.CreateModel();

// This declares a queue named "hello". 
channel.QueueDeclare(queue: "hello",
                     durable: false, // The queue will not survive a server restart.
                     exclusive: false, // The queue can be used by other connections.
                     autoDelete: false, // The queue will not be deleted when the last consumer unsubscribes.
                     arguments: null); // No additional arguments are provided.

var message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);

// This publishes the message to the "hello" queue.
channel.BasicPublish(exchange: string.Empty, // The default exchange is used. (direct exchange)
                     routingKey: "hello", // The message is sent to the "hello" queue.
                     basicProperties: null, // No additional properties are set.
                     body: body); // The byte array of the message is sent.
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();