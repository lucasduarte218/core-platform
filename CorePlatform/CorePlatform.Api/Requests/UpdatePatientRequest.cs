namespace CorePlatform.Api.Requests
{
    public class UpdatePatientRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Complement { get; set; }
        public bool IsActive { get; set; }
    }
}