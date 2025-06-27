using CorePlatform.Application.DTOs;
using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Shared;
using Mapster;
using System.Reflection;

namespace CorePlatform.Application.UseCases.AppointmentUseCase
{
    public class CreateAppointmentUseCase : ICreateAppointmentUseCase
    {
        private readonly IAppointmentRepository _repository;
        private readonly IPatientRepository _patientRepository;

        public CreateAppointmentUseCase(IAppointmentRepository repository, IPatientRepository patientRepository)
        {
            _repository = repository;
            _patientRepository = patientRepository;
        }

        public async Task<Result<Appointment>> ExecuteAsync(CreateAppointmentDto dto)
        {
            var patient = await _patientRepository.GetByCpfAsync(dto.PatientCpf);

            if (patient == null)
                return Result<Appointment>.Failure("Paciente não encontrado.");

            if (dto.DateTime.Kind == DateTimeKind.Unspecified)
            {
                dto.DateTime = TimeZoneInfo.ConvertTimeToUtc(dto.DateTime, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            }
            else if (dto.DateTime.Kind == DateTimeKind.Local)
            {
                dto.DateTime = dto.DateTime.ToUniversalTime();
            }

            if (dto.DateTime > DateTime.UtcNow)
                return Result<Appointment>.Failure("Data e hora não podem ser futuras.");

            var appointment = dto.Adapt<Appointment>();
            appointment.Patient = patient;

            await _repository.AddAsync(appointment);
            return Result<Appointment>.Success(appointment);
        }
    }
}