using BikeStore.core.Domain.Notification;
using BikeStore.core.Type;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BikeStore.core.Domain.Notification {
  public class ForkNotification : Notification {

    [Key]
    public int IdxForkNotfication { get; set; }
    public string Dscr { get; set; }
    public Guid UserId { get; set; }
    public string ForksModel { get; set; }
    [ForeignKey("IdxUser")]
    public int IdxUser { get; set; }
    


    public ICollection<ForkNotificationImage> ForkNotficationImages { get; set; }

  }
}
