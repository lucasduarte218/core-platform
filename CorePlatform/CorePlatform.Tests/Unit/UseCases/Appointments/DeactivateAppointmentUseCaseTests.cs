using CorePlatform.Application.UseCases.AppointmentUseCase;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using Moq;

namespace CorePlatform.Tests.Unit.UseCases.Appointments;

[TestFixture]
public class DeactivateAppointmentUseCaseTests
{
    [Test]
    public async Task ShouldFail_WhenAppointmentNotFound()
    {
        var repo = new Mock<IAppointmentRepository>();
        repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Appointment?)null);

        var useCase = new DeactivateAppointmentUseCase(repo.Object);
        var result = await useCase.ExecuteAsync(Guid.NewGuid());

        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Atendimento não encontrado.", result.Error);
    }

    [Test]
    public async Task ShouldDeactivate_WhenFound()
    {
        var appt = new Appointment { IsActive = true };
        var repo = new Mock<IAppointmentRepository>();
        repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(appt);

        var useCase = new DeactivateAppointmentUseCase(repo.Object);
        var result = await useCase.ExecuteAsync(Guid.NewGuid());

        Assert.IsTrue(result.IsSuccess);
        Assert.IsFalse(appt.IsActive);
    }
}
