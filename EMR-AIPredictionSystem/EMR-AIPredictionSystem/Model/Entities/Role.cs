using System;
using System.Collections.Generic;

namespace EMR_AIPredictionSystem.Model.Entities;

public partial class Role
{
    public string Id { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public string? CreateUser { get; set; }

    public string? UpdateUser { get; set; }

    public string? DeleteUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
