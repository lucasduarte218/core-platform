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

    public async Task<IEnumerable<Appointment>> GetByPatientCpfAsync(string patientCpf)
        => await _context.Appointments
            .Where(a => a.PatientCpf == patientCpf)
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

    public async Task<IEnumerable<Appointment>> GetFilteredAsync(DateTime? start, DateTime? end, string? patientCpf, bool? isActive)
    {
        var query = _context.Appointments.AsQueryable();

        if (start.HasValue)
            query = query.Where(a => a.DateTime >= start.Value);

        if (end.HasValue)
            query = query.Where(a => a.DateTime <= end.Value);

        if (!string.IsNullOrEmpty(patientCpf))
            query = query.Where(a => a.PatientCpf == patientCpf);

        if (isActive.HasValue)
            query = query.Where(a => a.IsActive == isActive.Value);

        return await query.ToListAsync();
    }
}

