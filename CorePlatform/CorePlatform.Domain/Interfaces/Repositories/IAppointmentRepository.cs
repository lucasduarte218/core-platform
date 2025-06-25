using CorePlatform.Domain.Entities;

namespace CorePlatform.Domain.Interfaces.Repositories
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Appointment>> GetFilteredAsync(DateTime? start, DateTime? end, string? patientCpf, bool? isActive);
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Appointment appointment);
    }
}