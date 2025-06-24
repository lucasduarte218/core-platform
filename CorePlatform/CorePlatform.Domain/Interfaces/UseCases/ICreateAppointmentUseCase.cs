using CorePlatform.Domain.Entities;
using System.Threading.Tasks;

namespace CorePlatform.Domain.Interfaces.UseCases
{
    public interface ICreateAppointmentUseCase
    {
        Task<Appointment> ExecuteAsync(Appointment appointment);
    }
}