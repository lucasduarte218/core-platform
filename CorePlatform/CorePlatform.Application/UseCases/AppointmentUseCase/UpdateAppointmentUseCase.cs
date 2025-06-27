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
            return Result.Failure("Atendimento não encontrado.");

        if (dto.DateTime.HasValue)
        {
            if (dto.DateTime.Value.Kind == DateTimeKind.Unspecified)
            {
                dto.DateTime = TimeZoneInfo.ConvertTimeToUtc(dto.DateTime.Value, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            }
            else if (dto.DateTime.Value.Kind == DateTimeKind.Local)
            {
                dto.DateTime = dto.DateTime.Value.ToUniversalTime();
            }

            if (dto.DateTime.HasValue && dto.DateTime.Value > DateTime.UtcNow)
                return Result.Failure("Data e hora não podem ser futuras.");

            existing.DateTime = dto.DateTime.Value;
        }

        if (dto.PatientCpf is not null)
            existing.PatientCpf = dto.PatientCpf;

        if (dto.Description is not null)
            existing.Description = dto.Description;

        if (dto.IsActive.HasValue)
            existing.IsActive = dto.IsActive.Value;

        existing.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existing);
        return Result.Success();
    }
}