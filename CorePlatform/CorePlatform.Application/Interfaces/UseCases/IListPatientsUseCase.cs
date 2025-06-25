using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.Interfaces.UseCases;

public interface IListPatientsUseCase
{
    Task<Result<IEnumerable<Patient>>> ExecuteAsync(string? name, string? cpf, bool? isActive);
}