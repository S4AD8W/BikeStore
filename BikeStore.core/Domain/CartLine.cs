using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.core.Domain {
 public class CartLine {
    public int CartLineID { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
  }
}
