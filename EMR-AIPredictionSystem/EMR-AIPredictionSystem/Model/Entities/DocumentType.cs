using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class DocumentType
{
    public string Id { get; set; } = null!;

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public int? MaxSignatures { get; set; }

    public virtual ICollection<MedicalDocument> MedicalDocuments { get; set; } = new List<MedicalDocument>();
}
