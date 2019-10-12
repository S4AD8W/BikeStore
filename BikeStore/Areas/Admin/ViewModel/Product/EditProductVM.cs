using BikeStore.core.Domain.Product_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Areas.Admin.ViewModel.Product {
  public class EditProductVM {

    public IEnumerable<ProductImage> ProductImages { get; set; }
    public ProductCategory Category { get; set; }
    public int IdxProduct { get; set; }
    public string Name { get;  set; }
    public string Description { get;  set; }
    public decimal Price { get;  set; }
    public int IdxCategory { get;  set; }
    public DateTime CreateAt { get;  set; }

    public EditProductVM() {

    }

    public EditProductVM(BikeStore.core.Domain.Product_NS.Product xProduct, IEnumerable<ProductImage> xProductImages, ProductCategory xCategory) {
      this.ProductImages = xProductImages;
      this.Category = xCategory;
      this.IdxProduct = xProduct.IdxProduct;
      this.Name = xProduct.Name;
      this.Description = xProduct.Description;
      this.Price = xProduct.Price;
      this.CreateAt = xProduct.CreateAt;
    }
  }


}
