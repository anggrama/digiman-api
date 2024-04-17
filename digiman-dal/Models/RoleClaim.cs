using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class RoleClaim
{
    public Guid Id { get; set; }

    public Guid ClaimId { get; set; }

    public string? ClaimValue { get; set; }

    public Guid RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;
}
