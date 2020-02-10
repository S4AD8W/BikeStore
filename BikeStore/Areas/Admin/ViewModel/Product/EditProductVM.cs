using BikeStore.core.Domain.Product_NS;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Areas.Admin.ViewModel.Product {
  public class EditProductVM {

    public IEnumerable<ProductImage> ProductImages { get; set; }
    public IEnumerable< ProductCategory> Categories { get; set; }
    public int IdxProduct { get; set; }
    public string Name { get;  set; }
    public string Description { get;  set; }
    public double Price { get;  set; }
    public int IdxCategory { get;  set; }
    public DateTime CreateAt { get;  set; }
    public int Quantity { get;  set; }
    public List<SelectListItem> ProductCategories_SLI { get; set; }

    public EditProductVM() {

    }

    public EditProductVM(BikeStore.core.Domain.Product_NS.Product xProduct, IEnumerable<ProductImage> xProductImages, IEnumerable<ProductCategory> xCategories) {
      this.ProductImages = xProductImages;
      this.Categories = xCategories;
      this.IdxProduct = xProduct.IdxProduct;
      this.Name = xProduct.Name;
      this.Description = xProduct.Description;
      this.Price = xProduct.Price;
      this.CreateAt = xProduct.CreateAt;
      this.Quantity = xProduct.Quantity;
      this.ProductCategories_SLI =  xCategories.Select(x => new SelectListItem {
        Text = x.Name,
        Value = x.IdxProductCategory.ToString()
      }).ToList();
      

    }
  }


}
