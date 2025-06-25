using CorePlatform.Application.DTOs;
using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Shared;
using Mapster;

namespace CorePlatform.Application.UseCases.AppointmentUseCase;

public class UpdateAppointmentUseCase : IUpdateAppointmentUseCase
{
    private readonly IAppointmentRepository _repository;

    public UpdateAppointmentUseCase(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> ExecuteAsync(UpdateAppointmentDto dto)
    {
        Appointment? existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            return Result.Failure("Atendimento n�o encontrado.");

        if (dto.DateTime.HasValue && dto.DateTime.Value > DateTime.Now)
            return Result.Failure("Data e hora n�o podem ser futuras.");

        if (dto.PatientCpf is not null)
            existing.PatientCpf = dto.PatientCpf;

        if (dto.DateTime.HasValue)
            existing.DateTime = dto.DateTime.Value;

        if (dto.Description is not null)
            existing.Description = dto.Description;

        if (dto.IsActive.HasValue)
            existing.IsActive = dto.IsActive.Value;

        existing.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existing);
        return Result.Success();
    }
}