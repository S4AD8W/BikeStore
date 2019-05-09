using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Areas.Admin.ViewModel;
using BikeStore.Controllers;
using BikeStore.core.Domain.Notification;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.EF;
using BikeStore.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Areas.Admin.Controllers {
  [Area("Admin")]
  public class NotificationController : BikeStoreControllerBaseController {

    private readonly BikeStoreContext mDB;
    int mPageSize = 1;
    public NotificationController(ICommandDispatcher xCommandDispatcher, IMapper xMapper, BikeStoreContext xDB)
      : base (xCommandDispatcher,xMapper) {
      mDB = xDB;
    }
    public async Task<IActionResult> Fork(NotificationStatusEnum xStatus, int productpage = 1) {

      AllForkVM pVM = new AllForkVM();
      pVM.Forks = await mDB.ForksNotifications.ToListAsync();

      pVM.Forks = await mDB.ForksNotifications
                    .Where(p => xStatus != NotificationStatusEnum.New || p.NotificationStatus == xStatus)
                    //.OrderBy(p => p.ProductID)
                    .Skip((productpage - 1) * mPageSize)
                    .Take(mPageSize).ToListAsync();

      pVM.PagingInfo = new PagingInfo {
        CurrentPage = productpage,
        ItemsPerPage = mPageSize,
        TotalItems = xStatus != NotificationStatusEnum.New ?
                        mDB.ForksNotifications.Count() :
                        mDB.ForksNotifications.Where(e =>
                            e.NotificationStatus == xStatus).Count()
      };
      

      return View(pVM);
    }
  }
}