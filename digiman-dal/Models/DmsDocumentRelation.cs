using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class DmsDocumentRelation
{
    public Guid Id { get; set; }

    public Guid? DocumentId { get; set; }

    public Guid? DocumentRefId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual DmsDocument? Document { get; set; }

    public virtual DmsDocument? DocumentRef { get; set; }
}
