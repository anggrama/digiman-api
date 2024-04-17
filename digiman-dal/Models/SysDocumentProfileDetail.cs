using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysDocumentProfileDetail
{
    public Guid Id { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DocumentProfileId { get; set; }

    public string? FieldName { get; set; }

    public bool? Mandatory { get; set; }

    public string? FieldType { get; set; }

    public int? DisplaySeq { get; set; }

    public string? FieldOptions { get; set; }

    public virtual ICollection<DmsDocumentDetail> DmsDocumentDetails { get; set; } = new List<DmsDocumentDetail>();

    public virtual SysDocumentProfile? DocumentProfile { get; set; }
}
