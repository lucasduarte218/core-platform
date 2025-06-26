using CorePlatform.Application.DTOs;
using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.UseCases.AppointmentUseCase;

public class GetAppointmentDashboardUseCase : IGetAppointmentDashboardUseCase
{
    private readonly IAppointmentRepository _repository;

    public GetAppointmentDashboardUseCase(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<AppointmentDashboardDto>> ExecuteAsync()
    {
        int total = await _repository.GetTotalCountAsync();
        int today = await _repository.GetTodayCountAsync();

        AppointmentDashboardDto dto = new AppointmentDashboardDto
        {
            TotalAppointments = total,
            TodayAppointments = today
        };

        return Result<AppointmentDashboardDto>.Success(dto);
    }
}
