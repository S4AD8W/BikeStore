using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.core.Domain;
using BikeStore.core.Domain.Product_NS;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Cart;
using BikeStore.Infrastructure.Commands.Order;
using BikeStore.Infrastructure.Dispatcher;
using BikeStore.Infrastructure.Extensions;
using BikeStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  public class CartController : BikeStoreControllerBaseController {
    private IProductsRepository mProductRepository;
    private IProductImageRepository  mProductImageRepository;
    public CartController(IProductsRepository xProductRepository, IDispatcher xCommandDispatcher, IMapper xMapper, IProductImageRepository xProductImageRepository)
           : base(xCommandDispatcher, xMapper) {
      mProductRepository = xProductRepository;
      mProductImageRepository = xProductImageRepository;
    }

    public ViewResult Index(string returnUrl) {

      CartIndexVM pVM  = new CartIndexVM(mProductImageRepository, HttpContext.Session.GetCart());

      return View(pVM);
    }

    public async Task<RedirectToActionResult> Add(AddProductCommand xCommand) {

      await SendAsync(xCommand);

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

    public async Task<IActionResult> CreateOrder(CreateOrderCommand xCommand) {

      int pIdxUser;
      string pIdxUserStr;
     
      string pReturUri;
      string pUserEmail;

      pReturUri = string.Empty;
      pIdxUserStr = HttpContext.User.Claims.Where(x => x.Type == CustomClaim.IdxUser)
        .Select(x => x.Value).SingleOrDefault();

      pUserEmail = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Email)
        .Select(x => x.Value).SingleOrDefault();

      pIdxUser = 0;

      if (pIdxUserStr != null) {
        pIdxUser = Convert.ToInt32(pIdxUserStr);
      }

      
      xCommand.SetConnectionData(
        HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
        HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString(),
        pIdxUser,
        pUserEmail);

      await SendAsync(xCommand);

      if (CommandResult.IsSuccess) {
        Response.StatusCode = 200;
      } else {
        Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status503ServiceUnavailable;
      }

      //var pObj = new { Action = CommandResult.RedirectUrl.Action, Controller = CommandResult.RedirectUrl.Controller, PayType = pCommand.CntPayMethod };

      return Json("");
    }

  }
}