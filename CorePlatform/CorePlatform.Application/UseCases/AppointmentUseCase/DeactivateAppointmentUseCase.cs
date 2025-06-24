using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;

namespace CorePlatform.Application.UseCases.AppointmentUseCase
{
    public class DeactivateAppointmentUseCase : IDeactivateAppointmentUseCase
    {
        private readonly IAppointmentRepository _repository;

        public DeactivateAppointmentUseCase(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid appointmentId)
        {
            var appointment = await _repository.GetByIdAsync(appointmentId);
            if (appointment == null) throw new Exception("Atendimento não encontrado.");
            appointment.IsActive = false;
            await _repository.UpdateAsync(appointment);
        }
    }
}