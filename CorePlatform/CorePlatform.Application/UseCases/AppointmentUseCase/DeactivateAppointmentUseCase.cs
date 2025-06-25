using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.UseCases.AppointmentUseCase;

public class DeactivateAppointmentUseCase : IDeactivateAppointmentUseCase
{
    private readonly IAppointmentRepository _repository;

    public DeactivateAppointmentUseCase(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> ExecuteAsync(Guid appointmentId)
    {
        var appointment = await _repository.GetByIdAsync(appointmentId);
        if (appointment == null)
            return Result.Failure("Atendimento não encontrado.");

        appointment.IsActive = false;
        await _repository.UpdateAsync(appointment);
        return Result.Success();
    }
}