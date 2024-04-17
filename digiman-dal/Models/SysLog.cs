using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysLog
{
    public Guid Id { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? IpAddress { get; set; }

    public string? ErrorMessage { get; set; }
}
