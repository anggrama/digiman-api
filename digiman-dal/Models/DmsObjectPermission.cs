using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class DmsObjectPermission
{
    public Guid Id { get; set; }

    public Guid? ObjectId { get; set; }

    public string? UserType { get; set; }

    public Guid? UserId { get; set; }

    public string? PermissionSettings { get; set; }

    public virtual DmsObject? Object { get; set; }

    public virtual User? User { get; set; }
}
