using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string? Fullname { get; set; }

    public string? RecordStatus { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool IsLdap { get; set; }

    public string? ProfileSettings { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<DmsObjectPermission> DmsObjectPermissions { get; set; } = new List<DmsObjectPermission>();

    public virtual ICollection<DmsObject> DmsObjects { get; set; } = new List<DmsObject>();

    public virtual ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
