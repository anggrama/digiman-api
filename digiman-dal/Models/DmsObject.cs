using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class DmsObject
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public Guid? ParentId { get; set; }

    public Guid? RootId { get; set; }

    public int? HierarchyLevel { get; set; }

    public Guid? OwnerId { get; set; }

    public Guid? DocumentProfileId { get; set; }

    public string? DocumentProfileDefault { get; set; }

    public string? RecordStatus { get; set; }

    public string? Description { get; set; }

    public Guid? DefaultStorageId { get; set; }

    public int? LimitStorage { get; set; }

    public string? ScopeType { get; set; }

    public bool? IsInheritPermission { get; set; }

    public string? ObjectPermission { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual SysStorage? DefaultStorage { get; set; }

    public virtual ICollection<DmsDocument> DmsDocuments { get; set; } = new List<DmsDocument>();

    public virtual ICollection<DmsObjectPermission> DmsObjectPermissions { get; set; } = new List<DmsObjectPermission>();

    public virtual SysDocumentProfile? DocumentProfile { get; set; }

    public virtual ICollection<DmsObject> InverseRoot { get; set; } = new List<DmsObject>();

    public virtual DmsObject? Root { get; set; }
}
