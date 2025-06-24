using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.UseCases.AppointmentUseCase
{
    public class CreateAppointmentUseCase : ICreateAppointmentUseCase
    {
        private readonly IAppointmentRepository _repository;

        public CreateAppointmentUseCase(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Appointment>> ExecuteAsync(Appointment appointment)
        {
            if (appointment.DateTime > DateTime.Now)
                return Result<Appointment>.Failure("Data e hora não podem ser futuras.");

            await _repository.AddAsync(appointment);
            return Result<Appointment>.Success(appointment);
        }
    }
}