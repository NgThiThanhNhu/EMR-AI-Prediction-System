using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class Aidiagnosis
{
    public string Id { get; set; } = null!;

    public string MedicalRecordId { get; set; } = null!;

    public string? PredictedDisease { get; set; }

    public double? ConfidenceScore { get; set; }

    public string? SuggestedTreatment { get; set; }

    public string? Priority { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
