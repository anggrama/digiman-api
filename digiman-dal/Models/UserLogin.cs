using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class UserLogin
{
    public Guid Id { get; set; }

    public string? LoginProvider { get; set; }

    public string? ProviderKey { get; set; }

    public string? ProviderDisplayName { get; set; }

    public Guid UserId { get; set; }
}
