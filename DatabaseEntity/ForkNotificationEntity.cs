using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseEntity {
  public class ForkNotificationEntity {

    [Key]
    public int ID { get; set; }
    public string Dscr { get; set; }
    public Guid UserId { get; set; }
    public string ForksModel { get; set; }
    public byte[] ForksImage { get; set; }

  }
}
