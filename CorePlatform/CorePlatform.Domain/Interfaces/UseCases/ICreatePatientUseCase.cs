using CorePlatform.Domain.Entities;
using System.Threading.Tasks;

namespace CorePlatform.Domain.Interfaces.UseCases
{
    public interface ICreatePatientUseCase
    {
        Task<Patient> ExecuteAsync(Patient patient);
    }
}