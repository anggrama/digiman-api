using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class DmsDocumentPinned
{
    public Guid Id { get; set; }

    public Guid? DocumentId { get; set; }

    public Guid? UserId { get; set; }
}
