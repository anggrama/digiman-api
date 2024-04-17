using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class DmsDocumentRelation
{
    public Guid Id { get; set; }

    public Guid? DocumentId { get; set; }

    public Guid? DocumentRefId { get; set; }

    public virtual DmsDocument? Document { get; set; }

    public virtual DmsDocument? DocumentRef { get; set; }
}
