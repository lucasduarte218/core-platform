using CorePlatform.Application.DTOs;
using CorePlatform.Application.UseCases.PatientUseCase;
using CorePlatform.Domain.Interfaces.Repositories;
using Moq;

namespace CorePlatform.Tests.Unit.UseCases.Patients;

[TestFixture]
public class CreatePatientUseCaseTests
{
    [Test]
    public async Task ShouldFail_WhenCpfAlreadyExists()
    {
        var repo = new Mock<IPatientRepository>();
        repo.Setup(r => r.GetByCpfAsync("123")).ReturnsAsync(new Domain.Entities.Patient());

        var useCase = new CreatePatientUseCase(repo.Object);

        var dto = new CreatePatientDto { CPF = "123" };
        var result = await useCase.ExecuteAsync(dto);

        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("CPF já cadastrado.", result.Error);
    }

    [Test]
    public async Task ShouldCreate_WhenValid()
    {
        var repo = new Mock<IPatientRepository>();
        repo.Setup(r => r.GetByCpfAsync("123")).ReturnsAsync((Domain.Entities.Patient?)null);
        repo.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.Patient>())).Returns(Task.CompletedTask);

        var useCase = new CreatePatientUseCase(repo.Object);

        var dto = new CreatePatientDto { CPF = "123", Name = "Lucas" };
        var result = await useCase.ExecuteAsync(dto);

        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Value);
    }
}
