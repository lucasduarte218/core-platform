using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Domain.Interfaces.UseCases;

public interface ICreatePatientUseCase
{
    Task<Result<Patient>> ExecuteAsync(Patient patient);
}