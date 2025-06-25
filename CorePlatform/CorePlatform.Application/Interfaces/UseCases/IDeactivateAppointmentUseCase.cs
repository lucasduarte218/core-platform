using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.Interfaces.UseCases;

public interface IDeactivateAppointmentUseCase
{
    Task<Result> ExecuteAsync(Guid appointmentId);
}