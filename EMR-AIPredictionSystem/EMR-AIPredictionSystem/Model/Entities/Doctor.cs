using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class Doctor
{
    public string Id { get; set; } = null!;

    public Guid UserId { get; set; }

    public string DepartmentId { get; set; } = null!;

    public double? ExperienceYears { get; set; }

    public string? DigitalSignaturePath { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new List<DoctorSchedule>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual User User { get; set; } = null!;
}
