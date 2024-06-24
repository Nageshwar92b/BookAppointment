using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare("smsqueue", exclusive: false);

var appointments = new EventingBasicConsumer(channel);
appointments.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine("appointment message received: '" + message + "'");
};
//read the message
channel.BasicConsume(queue: "smsqueue", autoAck: true, consumer: appointments);
Console.ReadKey();