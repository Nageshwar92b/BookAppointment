using RabbitMQ.Client;

namespace Appointment.Service.Interfaces
{
    public interface IRebbitMqConnection
    {
        IConnection connection { get; }
    }
}
