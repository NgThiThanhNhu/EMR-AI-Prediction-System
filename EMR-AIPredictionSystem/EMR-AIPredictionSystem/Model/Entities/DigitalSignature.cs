using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class DigitalSignature
{
    public string Id { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public Guid SignedBy { get; set; }

    public DateTime? SignedAt { get; set; }

    public string? SignaturePath { get; set; }

    public int? SignOrder { get; set; }

    public virtual MedicalDocument Document { get; set; } = null!;

    public virtual User SignedByNavigation { get; set; } = null!;
}
