using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class UserClaim
{
    public Guid Id { get; set; }

    public Guid ClaimId { get; set; }

    public string? ClaimValue { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
