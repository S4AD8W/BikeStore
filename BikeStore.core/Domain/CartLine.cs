using System;
using System.Collections.Generic;
using System.Text;
using BikeStore.core.Domain.Product;

namespace BikeStore.core.Domain {
 public class CartLine {
    public int CartLineID { get; set; }
    public BikeStore.core.Domain.Product.Product  Product { get; set; }
    public int Quantity { get; set; }
  }
}
