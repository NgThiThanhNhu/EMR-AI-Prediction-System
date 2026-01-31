using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class PrescriptionDetail
{
    public string Id { get; set; } = null!;

    public string PrescriptionId { get; set; } = null!;

    public string MedicineName { get; set; } = null!;

    public string? MedicineCategory { get; set; }

    public int? Quantity { get; set; }

    public string? Dosage { get; set; }

    public double? MorningDose { get; set; }

    public double? AfternoonDose { get; set; }

    public double? EveningDose { get; set; }

    public double? NightDose { get; set; }

    public string? Note { get; set; }

    public virtual Prescription Prescription { get; set; } = null!;
}
