using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.core.Domain {

  public  class Product {
    [Key]
    public int ProductID { get; set; }
    public string Descryption { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }

  }
}
