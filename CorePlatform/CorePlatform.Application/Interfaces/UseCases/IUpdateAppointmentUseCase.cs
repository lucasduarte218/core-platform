using CorePlatform.Application.DTOs;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.Interfaces.UseCases;

public interface IUpdateAppointmentUseCase
{
    Task<Result> ExecuteAsync(UpdateAppointmentDto dto);
}