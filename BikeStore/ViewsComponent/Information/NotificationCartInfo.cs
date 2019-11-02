using BikeStore.Infrastructure.EF;
using BikeStore.Models;
using BikeStore.ViewModels.Information;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BikeStore.ViewsComponent.Information {

  [ViewComponent(Name = "NotificationCartInfo")]
  public class NotificationCartInfo : ViewComponent {

    public readonly BikeStoreContext mDB;
    public NotificationCartInfo(BikeStoreContext xDB) {
      mDB = xDB;
    }

    public async Task<IViewComponentResult> InvokeAsync() {

      NotificationCartInfoVM pVM = new NotificationCartInfoVM();
      pVM.ForkNotyficationCount = mDB.Notifications.Count(x => x.NotificationStatus == core.Domain.Notification_NS.NotificationStatusEnum.New);

      return View("NotificationCartInfo",pVM);
    }
  }
}
