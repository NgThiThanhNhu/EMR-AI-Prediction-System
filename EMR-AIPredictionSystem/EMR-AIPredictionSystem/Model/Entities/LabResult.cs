using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class LabResult
{
    public string Id { get; set; } = null!;

    public string MedicalRecordId { get; set; } = null!;

    public string? TestName { get; set; }

    public string? ResultValue { get; set; }

    public string? Unit { get; set; }

    public string? ReferenceRange { get; set; }

    public string? Evaluation { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
