using CorePlatform.Domain.Entities;
using System.Threading.Tasks;

namespace CorePlatform.Domain.Interfaces.UseCases
{
    public interface IUpdatePatientUseCase
    {
        Task ExecuteAsync(Patient patient);
    }
}