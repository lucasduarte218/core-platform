using CorePlatform.Domain.Entities;
using CorePlatform.Infrastructure.Repositories;

namespace CorePlatform.Tests.Integration.Repository
{
    [TestFixture]
    public class AppointmentRepositoryTests
    {
        [Test]
        public async Task ShouldAddAndRetrieveById()
        {
            var context = TestDbContextFactory.CreateInMemoryContext();
            var repo = new AppointmentRepository(context);

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientCpf = "123",
                DateTime = DateTime.Now,
                IsActive = true,
                Description = "Consulta geral"
            };

            await repo.AddAsync(appointment);

            var result = await repo.GetByIdAsync(appointment.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual("123", result!.PatientCpf);
        }

        [Test]
        public async Task ShouldFilterAppointments()
        {
            var context = TestDbContextFactory.CreateInMemoryContext();
            var repo = new AppointmentRepository(context);

            await repo.AddAsync(new Appointment
            {
                Id = Guid.NewGuid(),
                PatientCpf = "abc",
                DateTime = DateTime.Today.AddHours(9),
                IsActive = true,
                Description = "Atendimento A"
            });

            await repo.AddAsync(new Appointment
            {
                Id = Guid.NewGuid(),
                PatientCpf = "def",
                DateTime = DateTime.Today.AddDays(-1),
                IsActive = false,
                Description = "Atendimento B"
            });

            var filtered = await repo.GetFilteredAsync(DateTime.Today, null, "abc", true);

            Assert.AreEqual(1, filtered.Count());
        }

        [Test]
        public async Task ShouldReturnCorrectCounts()
        {
            var context = TestDbContextFactory.CreateInMemoryContext();
            var repo = new AppointmentRepository(context);

            await repo.AddAsync(new Appointment
            {
                Id = Guid.NewGuid(),
                PatientCpf = "1",
                DateTime = DateTime.Today.AddHours(10),
                IsActive = true,
                Description = "Consulta hoje"
            });

            await repo.AddAsync(new Appointment
            {
                Id = Guid.NewGuid(),
                PatientCpf = "2",
                DateTime = DateTime.Today.AddDays(-2),
                IsActive = true,
                Description = "Consulta passada"
            });

            int total = await repo.GetTotalCountAsync();
            int today = await repo.GetTodayCountAsync();

            Assert.AreEqual(2, total);
            Assert.AreEqual(1, today);
        }
    }
}
