using BikeStore.core.Type;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.core.Domain {
  public class ForkNotification {

    [Key]
    public int IdxForkNotfication { get; set; }
    public string Dscr { get; set; }
    public Guid UserId { get; set; }
    public string ForksModel { get; set; }

    ICollection<ForkNotficationImage> ForkNotficationImages { get; set; }

  }
}
