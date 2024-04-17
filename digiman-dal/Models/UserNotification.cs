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

    public virtual Notification? Notification { get; set; }
}
