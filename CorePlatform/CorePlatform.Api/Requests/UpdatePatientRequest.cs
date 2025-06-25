namespace CorePlatform.Api.Requests
{
    public class UpdatePatientRequest
    {
        public string CPF { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Address { get; set; }
        public string? Complement { get; set; }
        public bool? IsActive { get; set; }
    }
}