namespace CorePlatform.Application.DTOs
{
    public class CreateAppointmentDto
    {
        public string PatientCpf { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}