using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class DmsDocument
{
    public Guid Id { get; set; }

    public Guid ObjectId { get; set; }

    public DateOnly? EffectiveStartDate { get; set; }

    public DateOnly? EffectiveEndDate { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? Priority { get; set; }

    public bool IsCheckout { get; set; }

    public string? Version { get; set; }

    public string? ReminderSettings { get; set; }

    public bool IsObsolete { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<DmsDocumentDetail> DmsDocumentDetails { get; set; } = new List<DmsDocumentDetail>();

    public virtual ICollection<DmsDocumentFile> DmsDocumentFiles { get; set; } = new List<DmsDocumentFile>();

    public virtual ICollection<DmsDocumentRelation> DmsDocumentRelationDocumentRefs { get; set; } = new List<DmsDocumentRelation>();

    public virtual ICollection<DmsDocumentRelation> DmsDocumentRelationDocuments { get; set; } = new List<DmsDocumentRelation>();

    public virtual DmsObject Object { get; set; } = null!;
}
