using BikeStore.core.Domain.Product_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.ViewModels.Shop {
  public class ProductsListVM {
    public class ProductRowItem {
      public Product Product { get; set; }
      public ProductImage ProductImage { get; set; }
    }

    public IEnumerable<ProductRowItem> Products { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public string CurrentCategory { get; set; }
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
    public ProductImage ProductImage { get; set; }


  }
}
