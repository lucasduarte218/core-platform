namespace CorePlatform.Application.DTOs
{
    public class UpdateAppointmentDto
    {
        public Guid Id { get; set; }
        public string? PatientCpf { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}