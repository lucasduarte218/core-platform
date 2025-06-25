using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.Interfaces.UseCases;

public interface IUpdateAppointmentUseCase
{
    Task<Result> ExecuteAsync(Appointment appointment);
}