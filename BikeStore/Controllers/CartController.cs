using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.core.Domain;
using BikeStore.core.Domain.Product_NS;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Extensions;
using BikeStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers
{
  public class CartController : Controller {
    private IProductsRepository mProductRepository;

    public CartController(IProductsRepository xProductRepository) {
      mProductRepository = xProductRepository;
    }

    public ViewResult Index(string returnUrl) {
      return View(new CartIndexVM {
        Cart = GetCart(),
        ReturnUrl = returnUrl
      });
    }

    public async Task< RedirectToActionResult> Add(int productId, string returnUrl) {
      Product product = mProductRepository.Products
          .FirstOrDefault(p => p.IdxProduct == productId);

      if (product != null) {
        Cart cart = GetCart();
        cart.AddItem(product, 1);
        SaveCart(cart);
      }
      return RedirectToAction("Index");
    }


    public RedirectToActionResult RemoveFromCart(int productId,
            string returnUrl) {
      Product product = mProductRepository.Products
          .FirstOrDefault(p => p.IdxProduct == productId);

      if (product != null) {
        Cart cart = GetCart();
        cart.RemoveLine(product);
        SaveCart(cart);
      }
      return RedirectToAction("Index", new { returnUrl });
    }

    private Cart GetCart() {
      Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
      return cart;
    }

    private void SaveCart(Cart cart) {
      HttpContext.Session.SetJson("Cart", cart);
    }
  }
}