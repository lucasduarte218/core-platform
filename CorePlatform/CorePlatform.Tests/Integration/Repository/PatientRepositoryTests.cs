using CorePlatform.Domain.Entities;
using CorePlatform.Infrastructure.Repositories;

namespace CorePlatform.Tests.Integration.Repository
{
    [TestFixture]
    public class PatientRepositoryTests
    {
        [Test]
        public async Task ShouldAddAndRetrievePatientByCpf()
        {
            var context = TestDbContextFactory.CreateInMemoryContext();
            var repo = new PatientRepository(context);

            var patient = new Patient
            {
                CPF = "123",
                Name = "Lucas",
                IsActive = true,
                BirthDate = new DateTime(1990, 1, 1),
                Gender = "Masculino",
                ZipCode = "12345-678",
                City = "Cidade Teste",
                District = "Centro",
                Address = "Rua A, 123",
                Complement = "Apto 1"
            };

            await repo.AddAsync(patient);

            var retrieved = await repo.GetByCpfAsync("123");

            Assert.IsNotNull(retrieved);
            Assert.AreEqual("Lucas", retrieved!.Name);
        }

        [Test]
        public async Task ShouldUpdatePatient()
        {
            var context = TestDbContextFactory.CreateInMemoryContext();
            var repo = new PatientRepository(context);

            var patient = new Patient
            {
                CPF = "123",
                Name = "Lucas",
                IsActive = true,
                BirthDate = new DateTime(1990, 1, 1),
                Gender = "Masculino",
                ZipCode = "12345-678",
                City = "Cidade Teste",
                District = "Centro",
                Address = "Rua A, 123",
                Complement = "Apto 1"
            };

            await repo.AddAsync(patient);

            patient.Name = "Atualizado";
            await repo.UpdateAsync(patient);

            var updated = await repo.GetByCpfAsync("123");

            Assert.AreEqual("Atualizado", updated!.Name);
        }

        [Test]
        public async Task ShouldCountActivePatients()
        {
            var context = TestDbContextFactory.CreateInMemoryContext();
            var repo = new PatientRepository(context);

            await repo.AddAsync(new Patient
            {
                CPF = "1",
                Name = "Paciente Ativo",
                IsActive = true,
                BirthDate = new DateTime(1980, 1, 1),
                Gender = "Feminino",
                ZipCode = "00000-000",
                City = "Cidade A",
                District = "Bairro A",
                Address = "Rua A, 123",
                Complement = ""
            });

            await repo.AddAsync(new Patient
            {
                CPF = "2",
                Name = "Paciente Inativo",
                IsActive = false,
                BirthDate = new DateTime(1985, 2, 2),
                Gender = "Masculino",
                ZipCode = "11111-111",
                City = "Cidade B",
                District = "Bairro B",
                Address = "Rua B, 456",
                Complement = ""
            });

            int count = await repo.GetActiveCountAsync();
            Assert.AreEqual(1, count);
        }
    }
}
