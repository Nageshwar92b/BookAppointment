using Appointment.DAL;
using Appointment.DAL.RepositoriesImplementation;
using Appointment.Domain.Interfaces;
using Appointment.Domain.Interfaces.Repositories;
using Appointment.Domain.Model;
using Appointment.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace Appointment.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IUseCaseHandler<AppointmentEntity, string>), typeof(VerifyAndSaveAppointment));
            services.AddSingleton<IRebbitMqConnection>(new RebbitMqConnection());
            services.AddScoped<IRabitMQProducer,RabitMQProducer>();
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }        
    }
}
