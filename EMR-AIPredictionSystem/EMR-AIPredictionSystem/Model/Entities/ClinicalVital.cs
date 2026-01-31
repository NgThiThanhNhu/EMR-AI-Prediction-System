using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class ClinicalVital
{
    public string Id { get; set; } = null!;

    public string MedicalRecordId { get; set; } = null!;

    public string? BloodPressure { get; set; }

    public int? HeartRate { get; set; }

    public double? Temperature { get; set; }

    public int? SpO2 { get; set; }

    public double? Weight { get; set; }

    public int? Height { get; set; }

    public DateTime? RecordedAt { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
