using CorePlatform.Application.DTOs;
using CorePlatform.Application.UseCases.AppointmentUseCase;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using Moq;

namespace CorePlatform.Tests.Unit.UseCases.Appointments;

[TestFixture]
public class UpdateAppointmentUseCaseTests
{
    [Test]
    public async Task ShouldFail_WhenAppointmentNotFound()
    {
        var repo = new Mock<IAppointmentRepository>();
        repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Appointment?)null);

        var useCase = new UpdateAppointmentUseCase(repo.Object);
        var dto = new UpdateAppointmentDto { Id = Guid.NewGuid() };

        var result = await useCase.ExecuteAsync(dto);

        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Atendimento não encontrado.", result.Error);
    }

    [Test]
    public async Task ShouldFail_WhenDateInFuture()
    {
        var appt = new Appointment();
        var repo = new Mock<IAppointmentRepository>();
        repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(appt);

        var useCase = new UpdateAppointmentUseCase(repo.Object);
        var dto = new UpdateAppointmentDto { Id = Guid.NewGuid(), DateTime = DateTime.Now.AddMinutes(10) };

        var result = await useCase.ExecuteAsync(dto);

        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Data e hora não podem ser futuras.", result.Error);
    }

    [Test]
    public async Task ShouldUpdate_WhenValid()
    {
        var appt = new Appointment();
        var repo = new Mock<IAppointmentRepository>();
        repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(appt);

        var useCase = new UpdateAppointmentUseCase(repo.Object);
        var dto = new UpdateAppointmentDto
        {
            Id = Guid.NewGuid(),
            PatientCpf = "123",
            DateTime = DateTime.Now.AddMinutes(-5),
            Description = "teste",
            IsActive = true
        };

        var result = await useCase.ExecuteAsync(dto);

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual("123", appt.PatientCpf);
        Assert.AreEqual("teste", appt.Description);
    }
}
