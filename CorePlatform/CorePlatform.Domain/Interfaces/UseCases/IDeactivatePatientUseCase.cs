using CorePlatform.Domain.Shared;

namespace CorePlatform.Domain.Interfaces.UseCases;

public interface IDeactivatePatientUseCase
{
    Task<Result> ExecuteAsync(Guid patientId);
}