using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BikeStore.core.Domain.Notification_NS {
 public class NotificationImage {
    [Key]
    public int IdxNotoificationImage { get; set; }
    [ForeignKey("IdxNotfication")]
    public int IdxNotification { get; set; }
    public string Name { get; set; }
    public long Size { get; set; }
    public byte[] Content { get; set; }

  }
}
