using BikeStore.core.Domain.Product_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.ViewModels.Shop {
  public class DetailProductVM {

    public Product Product { get; set; }
    public IEnumerable<ProductImage> ProductImages { get; set; }

    public DetailProductVM() {

    }

    public DetailProductVM(Product xProduct, IEnumerable<ProductImage> xProductImage) {
      this.Product = xProduct;
      this.ProductImages = xProductImage;
    }


  }
}
