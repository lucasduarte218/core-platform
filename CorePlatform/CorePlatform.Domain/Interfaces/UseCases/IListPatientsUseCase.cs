using CorePlatform.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorePlatform.Domain.Interfaces.UseCases
{
    public interface IListPatientsUseCase
    {
        Task<IEnumerable<Patient>> ExecuteAsync(string? name, string? cpf, bool? isActive);
    }
}