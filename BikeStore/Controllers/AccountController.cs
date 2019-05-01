using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Accounts;
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

    public AccountController(ICommandDispatcher xCommandDispatcher, IMessage xMessage, IMemoryCache xCache, IMapper xMapper)
           : base(xCommandDispatcher, xMapper) {
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

      return RedirectToAction("RegisterSuccess", "Account");

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

    public IActionResult ResetPassword() => View();         //funkcja odpowiadająca za wyświetlenie widoku do restartu hasła 

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPassword xResetPassword) {
      //funkcja odpowiadająca za restart chasła 
      //xResetPassword - komenda odpowiadająca za restart hasła 

      if (!ModelState.IsValid) {                            //sprawdzenie czy komeda  sostąła prawidłowo przez użytkownika uzupełniona 
        return View(xResetPassword);                        //zwrucenie widoku 
      }

      await DispatchAsync(xResetPassword);                  //wywołanie komendy 

      if (mMessage.IsMessage) {                             //sprawdzenie czy serwis nie zwrucił komunikatu 
        ViewBag.Error = mMessage.Message;
        return View();                                      //zwrucenie widoku z komunikatem 
      }

      //TODO:Przekierować na okno z informacją o zmianie chasła
      return View();

    }

    public IActionResult RegisterSuccess() => View();

  }
}