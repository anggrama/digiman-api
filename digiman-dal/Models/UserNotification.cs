using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class UserNotification
{
    public Guid Id { get; set; }

    public Guid? NotificationId { get; set; }

    public Guid? UserId { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? ReadDate { get; set; }

    public bool? IsNew { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Notification? Notification { get; set; }
}
