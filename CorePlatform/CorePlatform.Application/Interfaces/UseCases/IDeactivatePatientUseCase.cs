using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.Interfaces.UseCases;

public interface IDeactivatePatientUseCase
{
    Task<Result> ExecuteAsync(string cpf);
}