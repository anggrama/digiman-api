using System;
using System.Collections.Generic;

namespace digiman_dal.Models;

public partial class Notification
{
    public Guid Id { get; set; }

    public Guid? Message { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}
