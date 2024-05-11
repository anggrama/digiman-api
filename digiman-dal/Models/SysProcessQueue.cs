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

    public DateTime? FinishedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
}
