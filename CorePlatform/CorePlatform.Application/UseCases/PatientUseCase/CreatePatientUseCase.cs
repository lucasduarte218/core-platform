using CorePlatform.Application.DTOs;
using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Shared;
using Mapster;

namespace CorePlatform.Application.UseCases.PatientUseCase
{
    public class CreatePatientUseCase : ICreatePatientUseCase
    {
        private readonly IPatientRepository _repository;

        public CreatePatientUseCase(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Patient>> ExecuteAsync(CreatePatientDto patientDto)
        {
            var existing = await _repository.GetByCpfAsync(patientDto.CPF);

            if (existing != null)
                return Result<Patient>.Failure("CPF já cadastrado.");

            var patient = patientDto.Adapt<Patient>();

            await _repository.AddAsync(patient);
            return Result<Patient>.Success(patient);
        }
    }
}