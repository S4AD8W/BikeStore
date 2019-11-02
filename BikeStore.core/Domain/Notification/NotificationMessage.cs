using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BikeStore.core.Domain.Notification_NS {
 public class NotificationMessage {
    [Key]
    public int IdxNotificationMessage { get; private set; }
    [ForeignKey("IdxUser")]
    public int IdxUser { get; private set; }
    [ForeignKey("IdxNotfication")]
    public int IdxNotfication { get; private set; }
    public string Message { get; private set; }
    public DateTime CreateAT { get; private set; }

  }
}

