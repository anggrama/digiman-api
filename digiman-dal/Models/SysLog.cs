using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysLog
{
    public Guid Id { get; set; }

    public string? IpAddress { get; set; }

    public string? ErrorMessage { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
}
