using Appointment.Domain.Interfaces;
using Appointment.Domain.Interfaces.Repositories;
using Appointment.Domain.Model;
using Appointment.Service.Interfaces;

namespace Appointment.Service
{
    public class VerifyAndSaveAppointment : IUseCaseHandler<AppointmentEntity, string>
    {
        private readonly IRepository<AppointmentEntity> _appointmentRepository;
       // private readonly RabbitMQService _rabbitMQService;
        private readonly IRabitMQProducer _rabitMQProducer;
        public VerifyAndSaveAppointment(IRepository<AppointmentEntity> appointmentRepository, IRabitMQProducer rabitMQProducer)
        {
            _appointmentRepository = appointmentRepository;
            //_rabbitMQService = rabbitMQService;
            _rabitMQProducer = rabitMQProducer;
        }

        public async Task<string> Execute(AppointmentEntity request)
        {
            var response = string.Empty;
            try
            {
                AppointmentEntity data = await _appointmentRepository.FirstAsync(x => x.AppointmentDate == request.AppointmentDate);
                if (data == null)
                {
                    // _rabbitMQService.SendMessage(request);
                    var res = await _appointmentRepository.AddAsync(request);
                    _rabitMQProducer.SendMessage(request);
                    response = "Appointment Added Successfully";
                }
                else
                {
                    response = "Appointment Not Availble For Date '" + request.AppointmentDate + "'";
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return response;
        }
    }

}
