using CorePlatform.Application.UseCases.PatientUseCase;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using Moq;

namespace CorePlatform.Tests.Unit.UseCases.Patients;

[TestFixture]
public class ListPatientsUseCaseTests
{
    [Test]
    public async Task ShouldReturnFilteredPatients()
    {
        var repo = new Mock<IPatientRepository>();
        var data = new List<Patient>
    {
        new Patient { Name = "Lucas", CPF = "1", IsActive = true },
        new Patient { Name = "Maria", CPF = "2", IsActive = false }
    };

        repo.Setup(r => r.GetAllAsync()).ReturnsAsync(data);

        var useCase = new ListPatientsUseCase(repo.Object);

        var result = await useCase.ExecuteAsync("Lucas", null, null);
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(1, result.Value.Count());
    }
}
