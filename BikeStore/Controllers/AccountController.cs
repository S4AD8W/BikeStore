using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Users;
using BikeStore.Infrastructure.Extensions;
using BikeStore.Infrastructure.Services.Messages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

    public IActionResult Login() => View();                //funkcja zwracająca widok logowania 

    [HttpPost]
    public async Task<IActionResult> Login(LogIn xLogin, string returnUrl = null) {
      //funkcja odpowiadająca za zalogowanie się w serwisie 
      //xLogin - komenda z danymi logowania
      //TODO:Zmienić TokenID na UserID 

      await DispatchAsync(xLogin);                         //wywołanie Komendy 

      if (mMessage.IsMessage) {                             //sprawdzenie czy serwis nie zwrucił informacji
        ViewBag.Error = mMessage.IsMessage;
        return View();                                      //zwrucenie widoku z informacją 
      } else {
        var pToken = mCache.GetUserClaimsPrincipal(mMessage.UserId); //Odczytanie uprawnień użytkownika z pamięci serwera
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pToken); //zalogowanie użytkownika 
        return RedirectToAction("Index", "Home");           //przekierowanie użytkownika 
      }

    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> LogOut() {
      //funkcja wylogowująca użytkownika  

      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //wylogowanie użytkownika 

      return RedirectToAction("Index", "Home");             //przekierowanie do widoku głownego 

    }

  }
}