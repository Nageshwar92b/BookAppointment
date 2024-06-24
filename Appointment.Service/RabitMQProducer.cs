using Appointment.Service.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Appointment.Service
{
    public class RabitMQProducer : IRabitMQProducer
    {
        private readonly IRebbitMqConnection _connection;
        public RabitMQProducer(IRebbitMqConnection connection)
        {
            _connection = connection;
        }

        public void SendMessage<T>(T message)
        {
            var channel = _connection.connection.CreateModel();
            channel.QueueDeclare("smsqueue",exclusive:false);
           var jsonData= JsonConvert.SerializeObject(message);
            var smsBody=Encoding.UTF8.GetBytes(jsonData);
            channel.BasicPublish(exchange :"",routingKey:"smsqueue",body:smsBody); 
                
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            //ConnectionFactory factory = new ConnectionFactory
            //{
            //    HostName = "localhost"
            //};
            ////Create the RabbitMQ connection using connection factory details as i mentioned above
            //var connection = factory.CreateConnection();
            ////Here we create channel with session and model
            //using
            //var channel = connection.CreateModel();
            ////declare the queue after mentioning name and a few property related to that
            //channel.QueueDeclare("appointment", exclusive: false);
            ////Serialize the message
            //var json = JsonConvert.SerializeObject(message);
            //var body = Encoding.UTF8.GetBytes(json);
            ////put the data on to the product queue
            //channel.BasicPublish(exchange: "", routingKey: "appointment", body: body);
        }
    }
}
