using System.ComponentModel.DataAnnotations;

namespace CorePlatform.Domain.Entities
{
    public class Patient : BaseEntity
    {
        [Key]
        public string CPF { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Complement { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}