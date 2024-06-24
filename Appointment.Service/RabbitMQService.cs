using Appointment.Service.DTOModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Appointment.Service
{
    public class RabbitMQService
    {
        private readonly RabbitMQSettingsDTO _settings;
        private IConnection _connection;
        private IModel _channel;

        public delegate void MessageReceivedHandler(string message);
        public event MessageReceivedHandler OnMessageReceived;
        public RabbitMQService(IOptions<RabbitMQSettingsDTO> options)
        {
            _settings = options.Value;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _settings.QueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            try
            {
                _channel.BasicPublish(exchange: "",
                                      routingKey: _settings.QueueName,
                                      basicProperties: null,
                                      body: body);
            }
            catch (Exception ex)
            {

                //ex.Message;
            }

        }

        public void ReceiveMessages()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received message: {0}", message);
            };
            _channel.BasicConsume(queue: _settings.QueueName,
                                  autoAck: true,
                                  consumer: consumer);
        }
        public void StartReceivingMessages()
        {
            try
            {
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Message received: {message}");
                    OnMessageReceived?.Invoke(message);
                };
                _channel.BasicConsume(queue: _settings.QueueName,
                                      autoAck: true,
                                      consumer: consumer);
                Console.WriteLine("Started receiving messages.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving messages: {ex.Message}");
            }
        }
        public async Task<string> GetAllMsgAsync()
        {
            var message = string.Empty;
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                dynamic response = JsonConvert.DeserializeObject(message);
                message = response.data;
                //   OnMessageReceived?.Invoke(message); // Trigger the event
            };
            return message;
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }

}
