using CorePlatform.Application.DTOs;
using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Interfaces.Repositories;
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

        public async Task<Result> ExecuteAsync(UpdatePatientDto patientDto)
        {
            var existing = await _repository.GetByCpfAsync(patientDto.CPF);

            if (existing == null)
                return Result.Failure("Paciente não encontrado.");

            // Atualiza apenas os campos enviados (não nulos)
            if (patientDto.Name is not null)
                existing.Name = patientDto.Name;

            if (patientDto.BirthDate.HasValue)
                existing.BirthDate = patientDto.BirthDate.Value;

            if (patientDto.Gender is not null)
                existing.Gender = patientDto.Gender;

            if (patientDto.ZipCode is not null)
                existing.ZipCode = patientDto.ZipCode;

            if (patientDto.City is not null)
                existing.City = patientDto.City;

            if (patientDto.District is not null)
                existing.District = patientDto.District;

            if (patientDto.Address is not null)
                existing.Address = patientDto.Address;

            if (patientDto.Complement is not null)
                existing.Complement = patientDto.Complement;

            if (patientDto.IsActive.HasValue)
                existing.IsActive = patientDto.IsActive.Value;

            await _repository.UpdateAsync(existing);
            return Result.Success();
        }
    }
}