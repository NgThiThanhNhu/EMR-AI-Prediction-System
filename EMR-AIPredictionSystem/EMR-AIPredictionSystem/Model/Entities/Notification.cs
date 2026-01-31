using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class Notification
{
    public string Id { get; set; } = null!;

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? NotificationType { get; set; }

    public string? Priority { get; set; }

    public string? ReferenceId { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
