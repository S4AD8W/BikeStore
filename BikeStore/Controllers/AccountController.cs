using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Users;
using BikeStore.Infrastructure.Services.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BikeStore.Controllers {
  public class AccountController : BikeStoreControllerBaseController {
    private readonly IMessage mMessage;
    private readonly IMemoryCache mCache;

    public AccountController(ICommandDispatcher xCommandDispatcher, IMessage xMessage, IMemoryCache xCache)
           : base(xCommandDispatcher) {
      mMessage = xMessage;
      mCache = xCache;
    }


    public IActionResult Register()
      => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(CreateUser xCreateUser) {

      if (!ModelState.IsValid) {
        return View(xCreateUser);
      }

      await DispatchAsync(xCreateUser);
      if (mMessage.IsMessage) {
        ViewBag.Error = mMessage.Message;
        return View();
      }

      return RedirectToAction("Index", "Home");
    }

  }
}