using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class SysNotification
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? NotificationType { get; set; }

    public string? DefaultMessage { get; set; }

    public string? AvailableParameter { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
}
