using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;

namespace CorePlatform.Application.UseCases.AppointmentUseCase;

public class UpdateAppointmentUseCase : IUpdateAppointmentUseCase
{
    private readonly IAppointmentRepository _repository;

    public UpdateAppointmentUseCase(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Appointment appointment)
    {
        if (appointment.DateTime > DateTime.Now)
            throw new Exception("Data e hora não podem ser futuras.");

        await _repository.UpdateAsync(appointment);
    }
}