using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class MedicalRecord
{
    public string Id { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public string? DoctorId { get; set; }

    public string? DepartmentId { get; set; }

    public string? AppointmentId { get; set; }

    public string? Symptoms { get; set; }

    public string? ClinicalNotes { get; set; }

    public string? Diagnosis { get; set; }

    public string? TreatmentStatus { get; set; }

    public string? RecordStatus { get; set; }

    public DateTime? VisitAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? PaidAt { get; set; }

    public Guid? PaymentConfirmedByUserId { get; set; }

    public virtual ICollection<Aidiagnosis> Aidiagnoses { get; set; } = new List<Aidiagnosis>();

    public virtual Appointment? Appointment { get; set; }

    public virtual ICollection<ClinicalVital> ClinicalVitals { get; set; } = new List<ClinicalVital>();

    public virtual Department? Department { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual User? PaymentConfirmedByUser { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
