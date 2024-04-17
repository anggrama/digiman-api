using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysStorageType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? DisplayName { get; set; }

    public string? OptionsTemplate { get; set; }

    public string? IconClass { get; set; }

    public virtual ICollection<SysStorage> SysStorages { get; set; } = new List<SysStorage>();
}
