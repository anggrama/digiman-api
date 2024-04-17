using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysClaim
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? DisplayName { get; set; }

    public int? Level { get; set; }

    public Guid? ParentId { get; set; }

    /// <summary>
    /// M=Module C = Category V = View
    /// </summary>
    public string? ClaimsType { get; set; }

    public string? ClaimsTemplateValue { get; set; }
}
