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

namespace BikeStore.Controllers {
  public class CartController : BikeStoreControllerBaseController {
    private IProductsRepository mProductRepository;
    private IProductImageRepository  mProductImageRepository;
    public CartController(IProductsRepository xProductRepository, ICommandDispatcher xCommandDispatcher, IMapper xMapper, IProductImageRepository xProductImageRepository)
           : base(xCommandDispatcher, xMapper) {
      mProductRepository = xProductRepository;
      mProductImageRepository = xProductImageRepository;
    }

    public ViewResult Index(string returnUrl) {

      CartIndexVM pVM  = new CartIndexVM(mProductImageRepository, HttpContext.Session.GetCart());

      return View(pVM);
    }

    public async Task<RedirectToActionResult> Add(AddProductCommand xCommand) {

      await DispatchAsync(xCommand);

      if (!CommandResult.IsSuccess) {

      }

      return RedirectToAction("Index");
    }

    public async Task ChangeQuantiti (int xIdxProduct, int xQuantiti) {

      var pCart = HttpContext.Session.GetCart();

      if (!pCart.ChangeQuantiti(xIdxProduct, xQuantiti)) Response.StatusCode = 405;

      HttpContext.Session.SetJson(SessionEnum.Cart.ToString(), pCart);
      Response.StatusCode = 200;

      await Task.CompletedTask;

    }
    
    public async Task RemoveItem (int xIdxProduct) {

      var pCart = HttpContext.Session.GetCart();

      if (!pCart.RemoveItem(xIdxProduct)) Response.StatusCode = 405;

      HttpContext.Session.SetJson(SessionEnum.Cart.ToString(), pCart);
      Response.StatusCode = 200;

      await Task.CompletedTask;
    }


  }
}