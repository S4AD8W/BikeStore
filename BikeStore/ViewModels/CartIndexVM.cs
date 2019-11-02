using BikeStore.core.Domain;
using BikeStore.core.Domain.Product_NS;
using BikeStore.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.ViewModels {
  public class CartIndexVM {

    public class CartLineVM {
      public Product Product { get; set; }
      public int Quantiti { get; set; }
      public byte[] Picture { get; set; }
    }

    public string ReturnUrl { get; set; }
    public List<CartLineVM> CartLines { get; set; }
    public decimal TotalValue { get; set; }

    public CartIndexVM(IProductImageRepository xProducImageRepository, Cart xCart) {

      if (xCart != null) {
        this.CartLines = new List<CartLineVM>();
        this.TotalValue = xCart.ComputeTotalValue();
        foreach (var pCartLine in xCart.Lines) {
          this.CartLines.Add(new CartLineVM {
            Product = pCartLine.Product,
            Quantiti = pCartLine.Quantity,
            Picture = xProducImageRepository.ProductsImages.FirstOrDefault(x => x.IdxProduct == pCartLine.Product.IdxProduct).Content
          });
        }
      }


    }

  }
}
