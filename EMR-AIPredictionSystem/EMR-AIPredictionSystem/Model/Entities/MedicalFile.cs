using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class MedicalFile
{
    public string Id { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public int Year { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }
    public string Status { get; set; }

    public virtual ICollection<MedicalDocument> MedicalDocuments { get; set; } = new List<MedicalDocument>();

    public virtual Patient Patient { get; set; } = null!;

    public string FileCategoryId { get; set; }
    public FileCategory FileCategory { get; set; }
}
