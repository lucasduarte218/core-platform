namespace CorePlatform.Domain.Entities;

public class Appointment : BaseEntity
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; }
}