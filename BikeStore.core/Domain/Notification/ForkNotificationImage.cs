using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BikeStore.core.Domain.Notification {

  public  class ForkNotificationImage {
    [Key]
    public int IdxForkNotoificationImage { get; set; }
    [ForeignKey("IdxForkNotfication")]
    public int IdxForkNotfication { get; set; }
    public string Name { get; set; }
    public long Size { get; set; }
    public byte[] Content { get; set; }
  }

}
