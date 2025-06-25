using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.UseCases.AppointmentUseCase;

public class ListAppointmentsUseCase : IListAppointmentsUseCase
{
    private readonly IAppointmentRepository _repository;

    public ListAppointmentsUseCase(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Appointment>>> ExecuteAsync(DateTime? start, DateTime? end, string? patientCpf, bool? isActive)
    {
        var appointments = await _repository.GetFilteredAsync(start, end, patientCpf, isActive);

        return Result<IEnumerable<Appointment>>.Success(appointments);
    }
}