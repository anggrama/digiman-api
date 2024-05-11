﻿using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysStorageType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? DisplayName { get; set; }

    public string? OptionsTemplate { get; set; }

    public string? IconClass { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<SysStorage> SysStorages { get; set; } = new List<SysStorage>();
}
