using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.UseCases.AppointmentUseCase;

public class UpdateAppointmentUseCase : IUpdateAppointmentUseCase
{
    private readonly IAppointmentRepository _repository;

    public UpdateAppointmentUseCase(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> ExecuteAsync(Appointment appointment)
    {
        var existing = await _repository.GetByIdAsync(appointment.Id);
        if (existing == null)
            return Result.Failure("Atendimento não encontrado.");

        if (appointment.DateTime > DateTime.Now)
            return Result.Failure("Data e hora não podem ser futuras.");

        await _repository.UpdateAsync(appointment);
        return Result.Success();
    }
}