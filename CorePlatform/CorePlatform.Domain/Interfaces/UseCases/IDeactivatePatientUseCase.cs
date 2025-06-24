using System;
using System.Threading.Tasks;

namespace CorePlatform.Domain.Interfaces.UseCases
{
    public interface IDeactivatePatientUseCase
    {
        Task ExecuteAsync(Guid patientId);
    }
}