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

    // ✅ FK tới MedicalDocument (1:1)
    public string MedicalDocumentId { get; set; } = null!;

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

    public virtual MedicalDocument MedicalDocument { get; set; } = null!;

    public virtual Appointment? Appointment { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual User? PaymentConfirmedByUser { get; set; }

    // 1:1 (logic)
    public virtual ClinicalVital? ClinicalVital { get; set; }

    // 1:N
    public virtual ICollection<LabResult> LabResults { get; set; } = new List<LabResult>();

    public virtual ICollection<Aidiagnosis> Aidiagnoses { get; set; } = new List<Aidiagnosis>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
