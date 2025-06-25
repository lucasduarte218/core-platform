using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.Interfaces.UseCases;

public interface IListAppointmentsUseCase
{
    Task<Result<IEnumerable<Appointment>>> ExecuteAsync(DateTime? start, DateTime? end, string? patientCpf, bool? isActive);
}