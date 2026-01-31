using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class Person
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FullName { get; set; } = null!;

    public bool? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? AvatarPath { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual User? User { get; set; }
}
