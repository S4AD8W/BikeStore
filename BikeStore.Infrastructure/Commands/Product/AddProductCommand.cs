using BikeStore.core.Domain.Product_NS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Product {
  public class AddProductCommand : ICommand {

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int IdxCategory { get; set; }
    public List<ProductImage> Images { get; set; }


  }
}
