using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Interfaces.UseCases;
using CorePlatform.Domain.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorePlatform.Application.UseCases.PatientUseCase
{
    public class ListPatientsUseCase : IListPatientsUseCase
    {
        private readonly IPatientRepository _repository;

        public ListPatientsUseCase(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<Patient>>> ExecuteAsync(string? name, string? cpf, bool? isActive)
        {
            var patients = await _repository.GetAllAsync();
            var filtered = patients
                .Where(p => (string.IsNullOrEmpty(name) || p.Name.Contains(name))
                         && (string.IsNullOrEmpty(cpf) || p.CPF == cpf)
                         && (!isActive.HasValue || p.IsActive == isActive.Value));
            return Result<IEnumerable<Patient>>.Success(filtered);
        }
    }
}