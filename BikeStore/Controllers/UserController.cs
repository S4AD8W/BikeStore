using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Extensions;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Users;
using BikeStore.Infrastructure.EF;
using BikeStore.Infrastructure.Services;
using BikeStore.ViewModels.Notfication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  [Authorize]
  public class UserController : BikeStoreControllerBaseController {

    private readonly BikeStoreContext mDB;
    private readonly IUserService mUserService;

    public UserController(ICommandDispatcher xCommandDispatcher, IMapper xMapper, BikeStoreContext xDB, IUserService xUserService)
           : base(xCommandDispatcher, xMapper) {
      mDB = xDB;
      mUserService = xUserService;
    }


    public IActionResult UserAccount() {

      return View("UserAccount");
    }


    public IActionResult ForkNotification() {

      Guid pUserId;

      Guid.TryParse(User.Identity.Name, out pUserId);

      UserNotificationInfo pVM = new UserNotificationInfo();

      pVM.Forks = mDB.ForksNotifications.Where(x => x.UserId == pUserId);

      return View("UserAccount", pVM);

    }

    [HttpGet]
    public IActionResult AccountDetails()
    => View();

    [HttpGet]
    public IActionResult ChangePassword() {

      return View();

    }

    [HttpGet]
    public IActionResult ChangeEmail() {

      return View();

    }

    [HttpPost]
    [Authorize]
    public async Task<bool> CheckOldPassword(string OldPassowrd) {

      var pUserIdetities = HttpContext.User.GetUserIdentities();

      if (!await mUserService.CheckPasswordIsValidAsync(OldPassowrd, pUserIdetities.IdxUser))
        return false;

      return true;


    }

    [HttpPost]
    [Authorize]
    public async Task<string> ChangeOldPassword(ChangePasswordCommand xCommand) {


      //TODO:W widoku przerobić na ajxa i dorobić alerty w stylu botstapowym 
      return "Success";
    }

  }
}