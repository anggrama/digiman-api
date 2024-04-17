using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class DmsDocumentFile
{
    public Guid Id { get; set; }

    public Guid? DocumentId { get; set; }

    public string? OriginalFilename { get; set; }

    public string? ConvertedFilename { get; set; }

    public string? Extension { get; set; }

    public string? ContentType { get; set; }

    public string? Annotation { get; set; }

    public bool IsEncrypt { get; set; }

    public bool IsOcr { get; set; }

    public long Size { get; set; }

    public virtual DmsDocument? Document { get; set; }
}
