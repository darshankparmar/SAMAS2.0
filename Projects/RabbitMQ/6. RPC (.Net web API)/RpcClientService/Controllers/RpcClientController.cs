using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace RpcClientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RpcClientController : ControllerBase
    {
        private readonly RpcClient _rpcClient;

        public RpcClientController(RpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> GetFibonacciNumber(string number)
        {
            var response = await _rpcClient.CallAsync(number);
            return Ok(response);
        }
    }

    public class RpcClient : IDisposable
    {
        private const string QUEUE_NAME = "rpc_queue";
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _replyQueueName;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _callbackMapper = new();

        public RpcClient()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _replyQueueName = _channel.QueueDeclare().QueueName;
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                if (!_callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                    return;
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
                tcs.TrySetResult(response);
            };
            _channel.BasicConsume(consumer: consumer, queue: _replyQueueName, autoAck: true);
        }

        public Task<string> CallAsync(string message)
        {
            IBasicProperties props = _channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = _replyQueueName;
            var messageBytes = Encoding.UTF8.GetBytes(message);
            var tcs = new TaskCompletionSource<string>();
            _callbackMapper.TryAdd(correlationId, tcs);

            _channel.BasicPublish(exchange: string.Empty, routingKey: QUEUE_NAME, basicProperties: props, body: messageBytes);
            return tcs.Task;
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
