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

    public async Task<Result<IEnumerable<Appointment>>> ExecuteAsync(DateTime? start, DateTime? end, Guid? patientId, bool? isActive)
    {
        var appointments = await _repository.GetAllAsync();
        var filtered = appointments
            .Where(a => (!start.HasValue || a.DateTime >= start.Value)
                     && (!end.HasValue || a.DateTime <= end.Value)
                     && (!patientId.HasValue || a.PatientId == patientId.Value)
                     && (!isActive.HasValue || a.IsActive == isActive.Value));
        return Result<IEnumerable<Appointment>>.Success(filtered);
    }
}