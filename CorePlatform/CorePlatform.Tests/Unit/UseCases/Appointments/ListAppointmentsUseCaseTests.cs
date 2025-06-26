using CorePlatform.Application.UseCases.AppointmentUseCase;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using Moq;

namespace CorePlatform.Tests.Unit.UseCases.Appointments
{
    [TestFixture]
    public class ListAppointmentsUseCaseTests
    {
        [Test]
        public async Task ShouldReturnFilteredAppointments()
        {
            var repo = new Mock<IAppointmentRepository>();
            var expected = new List<Appointment> { new Appointment(), new Appointment() };
            repo.Setup(r => r.GetFilteredAsync(null, null, null, null)).ReturnsAsync(expected);

            var useCase = new ListAppointmentsUseCase(repo.Object);
            var result = await useCase.ExecuteAsync(null, null, null, null);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Value.Count());
        }
    }

}
