using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class Appointment
{
    public string Id { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public string ScheduleId { get; set; } = null!;

    public string? AppointmentType { get; set; }

    public string? AppointmentStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual DoctorSchedule Schedule { get; set; } = null!;
}
