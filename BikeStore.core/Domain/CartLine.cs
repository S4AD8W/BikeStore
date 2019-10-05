using System;
using System.Collections.Generic;
using System.Text;
using BikeStore.core.Domain.Product_NS;

namespace BikeStore.core.Domain {
 public class CartLine {
    public int CartLineID { get; set; }
    public BikeStore.core.Domain.Product_NS.Product  Product { get; set; }
    public int Quantity { get; set; }
  }
}
