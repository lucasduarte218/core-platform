using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;

namespace CorePlatform.Application.UseCases.PatientUseCase
{
    public class CreatePatientUseCase : ICreatePatientUseCase
    {
        private readonly IPatientRepository _repository;

        public CreatePatientUseCase(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Patient> ExecuteAsync(Patient patient)
        {
            var existing = await _repository.GetByCpfAsync(patient.CPF);
            if (existing != null)
                throw new Exception("CPF já cadastrado.");

            await _repository.AddAsync(patient);
            return patient;
        }
    }
}