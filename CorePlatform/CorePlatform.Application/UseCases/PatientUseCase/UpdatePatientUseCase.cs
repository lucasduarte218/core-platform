using CorePlatform.Application.DTOs;
using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Shared;
using Mapster;

namespace CorePlatform.Application.UseCases.PatientUseCase
{
    public class UpdatePatientUseCase : IUpdatePatientUseCase
    {
        private readonly IPatientRepository _repository;

        public UpdatePatientUseCase(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> ExecuteAsync(UpdatePatientDto patientDto)
        {
            var existing = await _repository.GetByIdAsync(patientDto.Id);
            if (existing == null)
                return Result.Failure("Paciente não encontrado.");

            Patient patient = patientDto.Adapt<Patient>();

            await _repository.UpdateAsync(patient);
            return Result.Success();
        }
    }
}