using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.EF;
using BikeStore.ViewModels.Notfication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  [Authorize]
  public class UserController : BikeStoreControllerBaseController{

    private readonly BikeStoreContext mDB;

    public UserController(ICommandDispatcher xCommandDispatcher, IMapper xMapper, BikeStoreContext xDB)
           : base(xCommandDispatcher, xMapper) {
      mDB = xDB;
    } 

    
    public IActionResult UserAccount() {

      return View("UserAccount");
    }

    
    public IActionResult ForkNotification() {

      Guid pUserId;

      Guid.TryParse( User.Identity.Name, out pUserId);

     UserNotificationInfo pVM = new UserNotificationInfo();

      pVM.Forks = mDB.ForksNotifications.Where(x => x.UserId == pUserId);

      return View("UserAccount",pVM);
    }
  }
}