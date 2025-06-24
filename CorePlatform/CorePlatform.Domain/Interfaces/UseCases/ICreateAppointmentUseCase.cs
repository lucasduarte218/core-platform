using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Domain.Interfaces.UseCases;

public interface ICreateAppointmentUseCase
{
    Task<Result<Appointment>> ExecuteAsync(Appointment appointment);
}