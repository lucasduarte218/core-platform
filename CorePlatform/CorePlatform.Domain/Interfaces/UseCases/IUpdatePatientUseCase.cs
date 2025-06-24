using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Domain.Interfaces.UseCases;

public interface IUpdatePatientUseCase
{
    Task<Result> ExecuteAsync(Patient patient);
}