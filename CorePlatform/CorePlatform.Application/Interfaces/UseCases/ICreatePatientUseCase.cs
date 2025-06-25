using CorePlatform.Application.DTOs;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.Interfaces.UseCases;

public interface ICreatePatientUseCase
{
    Task<Result<Patient>> ExecuteAsync(CreatePatientDto dto);
}