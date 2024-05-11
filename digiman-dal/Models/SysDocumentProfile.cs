using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysDocumentProfile
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? WatermarkFile { get; set; }

    public string? RecordStatus { get; set; }

    public Guid? ParentProfileId { get; set; }

    public bool IsSetDocumentProfile { get; set; }

    public string? AutonumberFormat { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<DmsObject> DmsObjects { get; set; } = new List<DmsObject>();

    public virtual ICollection<SysDocumentProfileDetail> SysDocumentProfileDetails { get; set; } = new List<SysDocumentProfileDetail>();
}
