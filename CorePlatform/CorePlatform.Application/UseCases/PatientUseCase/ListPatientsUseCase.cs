using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;

namespace CorePlatform.Application.UseCases.PatientUseCase;

public class ListPatientsUseCase : IListPatientsUseCase
{
    private readonly IPatientRepository _repository;

    public ListPatientsUseCase(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Patient>> ExecuteAsync(string? name, string? cpf, bool? isActive)
    {
        var patients = await _repository.GetAllAsync();
        return patients
            .Where(p => (string.IsNullOrEmpty(name) || p.Name.Contains(name))
                     && (string.IsNullOrEmpty(cpf) || p.CPF == cpf)
                     && (!isActive.HasValue || p.IsActive == isActive.Value));
    }
}