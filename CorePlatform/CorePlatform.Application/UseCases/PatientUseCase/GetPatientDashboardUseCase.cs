using CorePlatform.Application.DTOs;
using CorePlatform.Application.Interfaces.UseCases;
using CorePlatform.Domain.Interfaces.Repositories;
using CorePlatform.Domain.Shared;

namespace CorePlatform.Application.UseCases.PatientUseCase;

public class GetPatientDashboardUseCase : IGetPatientDashboardUseCase
{
    private readonly IPatientRepository _repository;

    public GetPatientDashboardUseCase(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PatientDashboardDto>> ExecuteAsync()
    {
        int total = await _repository.GetTotalCountAsync();
        int active = await _repository.GetActiveCountAsync();

        PatientDashboardDto dto = new PatientDashboardDto
        {
            TotalPatients = total,
            ActivePatients = active
        };

        return Result<PatientDashboardDto>.Success(dto);
    }
}
