using CorePlatform.Application.UseCases.PatientUseCase;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using Moq;

namespace CorePlatform.Tests.Unit.UseCases.Patients;

[TestFixture]
public class DeactivatePatientUseCaseTests
{
    [Test]
    public async Task ShouldFail_WhenNotFound()
    {
        var repo = new Mock<IPatientRepository>();
        repo.Setup(r => r.GetByCpfAsync("123")).ReturnsAsync((Patient?)null);

        var useCase = new DeactivatePatientUseCase(repo.Object);
        var result = await useCase.ExecuteAsync("123");

        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Paciente não encontrado.", result.Error);
    }

    [Test]
    public async Task ShouldDeactivate_WhenFound()
    {
        var patient = new Patient { IsActive = true };
        var repo = new Mock<IPatientRepository>();
        repo.Setup(r => r.GetByCpfAsync("123")).ReturnsAsync(patient);
        repo.Setup(r => r.UpdateAsync(patient)).Returns(Task.CompletedTask);

        var useCase = new DeactivatePatientUseCase(repo.Object);
        var result = await useCase.ExecuteAsync("123");

        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(patient.IsActive);
    }
}
