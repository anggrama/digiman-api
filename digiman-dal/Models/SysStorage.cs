using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysStorage
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public bool IsActive { get; set; }

    public Guid? StorageTypeId { get; set; }

    public string? StorageOptions { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<DmsObject> DmsObjects { get; set; } = new List<DmsObject>();

    public virtual SysStorageType? StorageType { get; set; }
}
