using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BikeStore.core.Domain.Notification {
  public class ForkNotificationMessage {

    [Key]
    public int IdxForkNotyficationComment { get; set; }
    [ForeignKey("IdxUser")]
    public int IdxUser { get; set; }
    [ForeignKey("IdxForkNotfication")]
    public int IdxForkNotfication { get; set; }
    public string Message { get; set; }
  }
}
