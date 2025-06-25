using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Shared;
using CorePlatform.Application.DTOs;
using System.Threading.Tasks;

namespace CorePlatform.Application.Interfaces.UseCases;

public interface IUpdatePatientUseCase
{
    Task<Result> ExecuteAsync(UpdatePatientDto dto);
}