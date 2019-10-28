using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.core.Domain;
using BikeStore.core.Domain.Product_NS;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Cart;
using BikeStore.Infrastructure.Extensions;
using BikeStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers
{
  public class CartController : BikeStoreControllerBaseController {
    private IProductsRepository mProductRepository;

    public CartController(IProductsRepository xProductRepository,ICommandDispatcher xCommandDispatcher, IMapper xMapper)
           : base(xCommandDispatcher, xMapper) {
      mProductRepository = xProductRepository;
    }

    public ViewResult Index(string returnUrl) {
      return View(new CartIndexVM {
        Cart = GetCart(),
        ReturnUrl = returnUrl
      });
    }

    public async Task< RedirectToActionResult> Add(AddProductCommand xCommand) {

      await DispatchAsync(xCommand);

      if (!CommandResult.IsSuccess) {

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