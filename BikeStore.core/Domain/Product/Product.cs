using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.core.Domain.Product_NS {
  public class Product {
    public Product(int xIdxProduct, string xNe, string xDescription, decimal xPrice, int xIdxCategory) {
      IdxProduct = xIdxProduct;
      Name = xNe;
      Description = xDescription;
      Price = xPrice;
      IdxCategory = xIdxCategory;
    }

    [Key]
    public int IdxProduct { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int IdxCategory { get; private set; }
    public DateTime CreateAt { get; private set; }
    public DateTime EditAt { get; private set; }

  }


}
