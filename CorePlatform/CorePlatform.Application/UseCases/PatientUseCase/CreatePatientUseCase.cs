using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.UseCases.PatientUseCase
{
    public class CreatePatientUseCase : ICreatePatientUseCase
    {
        private readonly IPatientRepository _repository;

        public CreatePatientUseCase(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Patient>> ExecuteAsync(Patient patient)
        {
            var existing = await _repository.GetByCpfAsync(patient.CPF);
            if (existing != null)
                return Result<Patient>.Failure("CPF já cadastrado.");

            await _repository.AddAsync(patient);
            return Result<Patient>.Success(patient);
        }
    }
}