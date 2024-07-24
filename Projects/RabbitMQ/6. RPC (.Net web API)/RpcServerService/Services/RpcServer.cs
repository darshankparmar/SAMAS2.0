using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RpcServerService.Services
{
    public class RpcServer
    {
        private readonly IModel _channel;

        public RpcServer()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "rpc_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: "rpc_queue", autoAck: false, consumer: consumer);
            consumer.Received += (model, ea) =>
            {
                string response = string.Empty;
                var body = ea.Body.ToArray();
                var props = ea.BasicProperties;
                var replyProps = _channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var message = Encoding.UTF8.GetString(body);
                    int n = int.Parse(message);
                    response = Fib(n).ToString();
                }
                catch (Exception)
                {
                    response = "Number is not valid";
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    _channel.BasicPublish(exchange: string.Empty, routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };
        }

        private static int Fib(int n)
        {
            if (n is 0 or 1)
            {
                return n;
            }
            return Fib(n - 1) + Fib(n - 2);
        }
    }
}
