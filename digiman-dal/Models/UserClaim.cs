using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class UserClaim
{
    public Guid Id { get; set; }

    public Guid ClaimId { get; set; }

    public string? ClaimValue { get; set; }

    public Guid UserId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
