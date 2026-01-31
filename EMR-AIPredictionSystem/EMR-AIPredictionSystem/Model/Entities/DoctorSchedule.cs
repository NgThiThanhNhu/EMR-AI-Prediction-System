using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class DoctorSchedule
{
    public string Id { get; set; } = null!;

    public string DoctorId { get; set; } = null!;

    public DateOnly WorkDate { get; set; }

    public string? Shift { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int? MaxPatients { get; set; }

    public int? BookedPatients { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Doctor Doctor { get; set; } = null!;
}
