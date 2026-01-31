using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class Prescription
{
    public string Id { get; set; } = null!;

    public string MedicalRecordId { get; set; } = null!;

    public string? DiseaseCode { get; set; }

    public string? DiseaseName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
}
