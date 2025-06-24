using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CorePlatform.Infrastructure.Repositories;
public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment?> GetByIdAsync(Guid id)
        => await _context.Appointments.FindAsync(id);

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId)
        => await _context.Appointments
            .Where(a => a.PatientId == patientId)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetAllAsync()
        => await _context.Appointments.ToListAsync();

    public async Task AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Appointment appointment)
    {
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }
}

