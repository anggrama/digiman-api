using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysProcessQueue
{
    public Guid Id { get; set; }

    public string? QueueType { get; set; }

    public string? QueueContent { get; set; }

    public bool? IsError { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? FinishedDate { get; set; }
}
