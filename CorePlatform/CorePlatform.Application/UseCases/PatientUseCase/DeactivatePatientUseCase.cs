using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;

namespace CorePlatform.Application.UseCases.PatientUseCase
{
    public class DeactivatePatientUseCase : IDeactivatePatientUseCase
    {
        private readonly IPatientRepository _repository;

        public DeactivatePatientUseCase(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid patientId)
        {
            var patient = await _repository.GetByIdAsync(patientId);
            if (patient == null) throw new Exception("Paciente não encontrado.");
            patient.IsActive = false;
            await _repository.UpdateAsync(patient);
        }
    }
}