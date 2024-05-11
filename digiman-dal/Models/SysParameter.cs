using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysParameter
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public string? Type { get; set; }

    public string? DefaultValue { get; set; }

    public string? Description { get; set; }

    public string? VisibleStatus { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
}
