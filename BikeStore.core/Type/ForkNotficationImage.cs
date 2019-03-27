using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BikeStore.core.Type {
public  class ForkNotficationImage {
    [Key]
    public int IdxForkNotoficationImage { get; set; }
    [ForeignKey("IdxForkNotfication")]
    public int IdxForkNotfication { get; set; }
    public string FileName { get; set; }
    public int Size { get; set; }
  }
}
