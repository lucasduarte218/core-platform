using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CorePlatform.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Patient?> GetByIdAsync(Guid id)
        => await _context.Patients.FindAsync(id);

    public async Task<Patient?> GetByCpfAsync(string cpf)
        => await _context.Patients.FirstOrDefaultAsync(p => p.CPF == cpf);

    public async Task<IEnumerable<Patient>> GetAllAsync()
        => await _context.Patients.ToListAsync();

    public async Task AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Patient patient)
    {
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }
    public async Task<int> GetTotalCountAsync()
    => await _context.Patients.CountAsync();

    public async Task<int> GetActiveCountAsync()
        => await _context.Patients.CountAsync(p => p.IsActive);
}
