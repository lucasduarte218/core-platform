using CorePlatform.Application.DTOs;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.Interfaces.UseCases;

public interface IGetAppointmentDashboardUseCase
{
    Task<Result<AppointmentDashboardDto>> ExecuteAsync();
}
