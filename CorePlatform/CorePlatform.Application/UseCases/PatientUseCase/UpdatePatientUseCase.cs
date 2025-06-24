using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;

namespace CorePlatform.Application.UseCases.PatientUseCase;

public class UpdatePatientUseCase : IUpdatePatientUseCase
{
    private readonly IPatientRepository _repository;

    public UpdatePatientUseCase(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Patient patient)
    {
        await _repository.UpdateAsync(patient);
    }
}