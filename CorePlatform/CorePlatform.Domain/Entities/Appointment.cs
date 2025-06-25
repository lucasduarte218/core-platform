namespace CorePlatform.Domain.Entities;

public class Appointment : BaseEntity
{
    public Guid Id { get; set; }
    public string PatientCpf { get; set; } = null!;
    public Patient Patient { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; }
}