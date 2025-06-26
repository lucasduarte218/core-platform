using CorePlatform.Application.UseCases.PatientUseCase;
using CorePlatform.Domain.Interfaces.Repositories;
using Moq;

namespace CorePlatform.Tests.Unit.UseCases.Patients;

[TestFixture]
public class GetPatientDashboardUseCaseTests
{
    [Test]
    public async Task ShouldReturnCounts()
    {
        var repo = new Mock<IPatientRepository>();
        repo.Setup(r => r.GetTotalCountAsync()).ReturnsAsync(10);
        repo.Setup(r => r.GetActiveCountAsync()).ReturnsAsync(7);

        var useCase = new GetPatientDashboardUseCase(repo.Object);
        var result = await useCase.ExecuteAsync();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(10, result.Value.TotalPatients);
        Assert.AreEqual(7, result.Value.ActivePatients);
    }
}
