using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.UseCases.PatientUseCase
{
    public class DeactivatePatientUseCase : IDeactivatePatientUseCase
    {
        private readonly IPatientRepository _repository;

        public DeactivatePatientUseCase(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> ExecuteAsync(string cpf)
        {
            var patient = await _repository.GetByCpfAsync(cpf);
            if (patient == null)
                return Result.Failure("Paciente não encontrado.");

            patient.IsActive = false;
            await _repository.UpdateAsync(patient);
            return Result.Success();
        }
    }
}