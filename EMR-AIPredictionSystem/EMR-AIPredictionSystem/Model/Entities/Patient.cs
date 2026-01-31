using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class Patient : BaseEntity
{
    public string Id { get; set; } = null!;

    public Guid PersonId { get; set; }

    public string? InsuranceNumber { get; set; }

    //public string? CreateUser { get; set; }

    //public string? UpdateUser { get; set; }

    //public string? DeleteUser { get; set; }

    //public DateTime? CreateDate { get; set; }

    //public DateTime? UpdateDate { get; set; }

    //public DateTime? DeleteDate { get; set; }

    //public bool? IsDeleted { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<MedicalFile> MedicalFiles { get; set; } = new List<MedicalFile>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual Person Person { get; set; } = null!;
}
