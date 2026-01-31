using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class MedicalDocument
{
    public string Id { get; set; } = null!;

    public string MedicalFileId { get; set; } = null!;

    public string DocumentTypeId { get; set; } = null!;

    public string? RelatedEntityType { get; set; }

    public string? RelatedRecordId { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<DigitalSignature> DigitalSignatures { get; set; } = new List<DigitalSignature>();

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual MedicalFile MedicalFile { get; set; } = null!;
}
