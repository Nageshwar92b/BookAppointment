namespace Appointment.Service.DTOModels
{
    public class RabbitMQSettingsDTO
    {
        public string HostName { get; set; } = "http://localhost:15672";
        public string QueueName { get; set; } = "demoQueue";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
    }
}
