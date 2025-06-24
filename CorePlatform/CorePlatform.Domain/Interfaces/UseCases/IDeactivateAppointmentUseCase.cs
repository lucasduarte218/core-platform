using System;
using System.Threading.Tasks;

namespace CorePlatform.Domain.Interfaces.UseCases
{
    public interface IDeactivateAppointmentUseCase
    {
        Task ExecuteAsync(Guid appointmentId);
    }
}