using CorePlatform.Application.DTOs;
using CorePlatform.Application.UseCases.AppointmentUseCase;
using CorePlatform.Domain.Entities;
using CorePlatform.Domain.Interfaces.Repositories;
using Moq;

namespace CorePlatform.Tests.Unit.UseCases.Appointments
{

    [TestFixture]
    public class CreateAppointmentUseCaseTests
    {
        [Test]
        public async Task ShouldFail_WhenPatientNotFound()
        {
            var appointmentRepo = new Mock<IAppointmentRepository>();
            var patientRepo = new Mock<IPatientRepository>();
            patientRepo.Setup(r => r.GetByCpfAsync(It.IsAny<string>())).ReturnsAsync((Patient?)null);

            var useCase = new CreateAppointmentUseCase(appointmentRepo.Object, patientRepo.Object);

            var dto = new CreateAppointmentDto { PatientCpf = "123", DateTime = DateTime.Now };

            var result = await useCase.ExecuteAsync(dto);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Paciente não encontrado.", result.Error);
        }

        [Test]
        public async Task ShouldFail_WhenDateIsInFuture()
        {
            var patient = new Patient();
            var patientRepo = new Mock<IPatientRepository>();
            patientRepo.Setup(r => r.GetByCpfAsync("123")).ReturnsAsync(patient);

            var appointmentRepo = new Mock<IAppointmentRepository>();

            var useCase = new CreateAppointmentUseCase(appointmentRepo.Object, patientRepo.Object);

            var dto = new CreateAppointmentDto { PatientCpf = "123", DateTime = DateTime.Now.AddMinutes(10) };

            var result = await useCase.ExecuteAsync(dto);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Data e hora não podem ser futuras.", result.Error);
        }

        [Test]
        public async Task ShouldCreateAppointment_WhenDataIsValid()
        {
            var patient = new Patient();
            var patientRepo = new Mock<IPatientRepository>();
            patientRepo.Setup(r => r.GetByCpfAsync("123")).ReturnsAsync(patient);

            var appointmentRepo = new Mock<IAppointmentRepository>();
            appointmentRepo.Setup(r => r.AddAsync(It.IsAny<Appointment>())).Returns(Task.CompletedTask);

            var useCase = new CreateAppointmentUseCase(appointmentRepo.Object, patientRepo.Object);

            var dto = new CreateAppointmentDto { PatientCpf = "123", DateTime = DateTime.Now.AddMinutes(-10) };

            var result = await useCase.ExecuteAsync(dto);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
        }
    }

}
