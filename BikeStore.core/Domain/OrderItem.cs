using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BikeStore.core.Domain {
  public class OrderItem {

    [Key]
    public int IdxOrderItem { get; private set; }
    public int IdxOrder { get; private set; }
    public string Name { get; private set; }
    public int Quantiti { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int IdxProduct { get; private set; }
    [NotMapped]
    public decimal TotalPrice => Quantiti * UnitPrice;
  }
}
