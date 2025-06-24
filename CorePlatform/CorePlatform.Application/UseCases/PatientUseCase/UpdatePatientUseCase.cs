using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.UseCases.PatientUseCase
{
    public class UpdatePatientUseCase : IUpdatePatientUseCase
    {
        private readonly IPatientRepository _repository;

        public UpdatePatientUseCase(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> ExecuteAsync(Patient patient)
        {
            var existing = await _repository.GetByIdAsync(patient.Id);
            if (existing == null)
                return Result.Failure("Paciente não encontrado.");

            await _repository.UpdateAsync(patient);
            return Result.Success();
        }
    }
}