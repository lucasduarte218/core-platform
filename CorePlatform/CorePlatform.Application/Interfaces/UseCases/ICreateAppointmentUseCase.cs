using CorePlatform.Application.DTOs;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.Interfaces.UseCases;

public interface ICreateAppointmentUseCase
{
    Task<Result<Appointment>> ExecuteAsync(CreateAppointmentDto dto);
}