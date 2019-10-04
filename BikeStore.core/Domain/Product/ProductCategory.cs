using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BikeStore.core.Domain.Product {
  public class ProductCategory {

    [Key]
    public int IdxProductCategory { get; private set; }
    public string Name { get; private set; }
  }
}
