using Appointment.Domain.Interfaces;
using Appointment.Domain.Model;
using Appointment.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<string>> BookAppointment(IUseCaseHandler<AppointmentEntity, string> verifyAndSaveAppointment, AppointmentEntity request)
        {
            var response = string.Empty;
            try
            {

                response = await verifyAndSaveAppointment.Execute(request);
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }
            return response;
        }
        //[HttpGet]
        //public async Task<ActionResult<string>> GetAppointment()
        //{
        //    var response = "";
        //    return response;
        //}
    }
}
