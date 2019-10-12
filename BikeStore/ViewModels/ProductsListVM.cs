using BikeStore.core.Domain;
using BikeStore.core.Domain.Product_NS;
using BikeStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.ViewModels {
  public class ProductsListVM {

    public IEnumerable<Product> Products { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public string CurrentCategory { get; set; }
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
    
    

  }
}
