using CorePlatform.Domain.Shared;

namespace CorePlatform.Domain.Interfaces.UseCases;

public interface IDeactivateAppointmentUseCase
{
    Task<Result> ExecuteAsync(Guid appointmentId);
}