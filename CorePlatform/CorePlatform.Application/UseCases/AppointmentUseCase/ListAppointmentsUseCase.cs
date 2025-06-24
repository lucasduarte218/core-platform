using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;

namespace CorePlatform.Application.UseCases.AppointmentUseCase;

public class ListAppointmentsUseCase : IListAppointmentsUseCase
{
    private readonly IAppointmentRepository _repository;

    public ListAppointmentsUseCase(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Appointment>> ExecuteAsync(DateTime? start, DateTime? end, Guid? patientId, bool? isActive)
    {
        var appointments = await _repository.GetAllAsync();
        return appointments
            .Where(a => (!start.HasValue || a.DateTime >= start.Value)
                     && (!end.HasValue || a.DateTime <= end.Value)
                     && (!patientId.HasValue || a.PatientId == patientId.Value)
                     && (!isActive.HasValue || a.IsActive == isActive.Value));
    }
}