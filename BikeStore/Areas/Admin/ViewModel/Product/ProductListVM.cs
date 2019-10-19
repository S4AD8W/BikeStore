using BikeStore.core.Domain.Product_NS;
using BikeStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Areas.Admin.ViewModel.Product {
  public class ProductListVM {

    public IEnumerable<BikeStore.core.Domain.Product_NS.Product> Products { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public string CurrentCategory { get; set; }
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
  }
}
