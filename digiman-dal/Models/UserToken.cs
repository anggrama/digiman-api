using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class UserToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? LoginProvider { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
}
