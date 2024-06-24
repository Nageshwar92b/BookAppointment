namespace Appointment.Service.Interfaces
{
    public interface IRabitMQProducer
    {
        public void SendMessage<T>(T message);
    }
}
