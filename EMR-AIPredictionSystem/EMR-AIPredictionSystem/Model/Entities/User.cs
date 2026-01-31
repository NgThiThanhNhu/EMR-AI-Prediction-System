using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class User : BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string RoleId { get; set; } = null!;

    public Guid PersonId { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? Salt { get; set; }

    public bool? IsVerified { get; set; }

    public virtual ICollection<DigitalSignature> DigitalSignatures { get; set; } = new List<DigitalSignature>();

    public virtual Doctor? Doctor { get; set; }

    public virtual ICollection<MedicalFile> MedicalFiles { get; set; } = new List<MedicalFile>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Person Person { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
