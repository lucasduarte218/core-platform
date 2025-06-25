namespace CorePlatform.Api.Responses
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }
        public string PatientCpf { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}