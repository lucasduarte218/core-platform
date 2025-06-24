using CorePlatform.Domain.Entities;
using System.Threading.Tasks;

namespace CorePlatform.Domain.Interfaces.UseCases
{
    public interface IUpdateAppointmentUseCase
    {
        Task ExecuteAsync(Appointment appointment);
    }
}