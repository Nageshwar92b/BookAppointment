namespace Appointment.Domain.Interfaces
{
    public interface IUseCaseHandler<T, TO>
    {
        Task<TO> Execute(T request);
    }
}
