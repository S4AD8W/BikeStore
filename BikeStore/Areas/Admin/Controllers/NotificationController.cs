using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Areas.Admin.ViewModel;
using BikeStore.Controllers;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Areas.Admin.Controllers {
  [Area("Admin")]
  public class NotificationController : BikeStoreControllerBaseController {

    private readonly BikeStoreContext mDB;

    public NotificationController(ICommandDispatcher xCommandDispatcher, IMapper xMapper, BikeStoreContext xDB)
      : base (xCommandDispatcher,xMapper) {
      mDB = xDB;
    }
    public async Task<IActionResult> Fork() {

      AllForkVM pVM = new AllForkVM();
      pVM.Forks = await mDB.ForksNotifications.ToListAsync(); 

      return View(pVM);
    }
  }
}