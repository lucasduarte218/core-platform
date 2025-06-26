using CorePlatform.Application.UseCases.AppointmentUseCase;
using CorePlatform.Domain.Interfaces.Repositories;
using Moq;

namespace CorePlatform.Tests.Unit.UseCases.Appointments;

[TestFixture]
public class GetAppointmentDashboardUseCaseTests
{
    [Test]
    public async Task ShouldReturnCorrectCounts()
    {
        var repo = new Mock<IAppointmentRepository>();
        repo.Setup(r => r.GetTotalCountAsync()).ReturnsAsync(100);
        repo.Setup(r => r.GetTodayCountAsync()).ReturnsAsync(5);

        var useCase = new GetAppointmentDashboardUseCase(repo.Object);
        var result = await useCase.ExecuteAsync();

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(100, result.Value!.TotalAppointments);
        Assert.AreEqual(5, result.Value!.TodayAppointments);
    }
}
