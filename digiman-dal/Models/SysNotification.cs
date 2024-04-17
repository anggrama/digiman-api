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

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
