using CorePlatform.Application.DTOs;
using CorePlatform.Application.UseCases.PatientUseCase;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using Moq;

namespace CorePlatform.Tests.Unit.UseCases.Patients;

[TestFixture]
public class UpdatePatientUseCaseTests
{
    [Test]
    public async Task ShouldFail_WhenNotFound()
    {
        var repo = new Mock<IPatientRepository>();
        repo.Setup(r => r.GetByCpfAsync("123")).ReturnsAsync((Patient?)null);

        var useCase = new UpdatePatientUseCase(repo.Object);
        var dto = new UpdatePatientDto { CPF = "123" };

        var result = await useCase.ExecuteAsync(dto);

        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Paciente não encontrado.", result.Error);
    }

    [Test]
    public async Task ShouldUpdate_WhenValid()
    {
        var patient = new Patient();
        var repo = new Mock<IPatientRepository>();
        repo.Setup(r => r.GetByCpfAsync("123")).ReturnsAsync(patient);
        repo.Setup(r => r.UpdateAsync(It.IsAny<Patient>())).Returns(Task.CompletedTask);

        var useCase = new UpdatePatientUseCase(repo.Object);

        var dto = new UpdatePatientDto
        {
            CPF = "123",
            Name = "Novo Nome",
            City = "Cidade",
            ZipCode = "00000-000",
            IsActive = true
        };

        var result = await useCase.ExecuteAsync(dto);

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual("Novo Nome", patient.Name);
        Assert.AreEqual("Cidade", patient.City);
        Assert.AreEqual("00000-000", patient.ZipCode);
    }
}
