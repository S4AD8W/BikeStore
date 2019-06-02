using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.ViewsComponent.Notification {
  
 [ViewComponent(Name = "NotificationMessage")]
  public class NotificationMessage : ViewComponent {

    public NotificationMessage() {

    }

    public async Task<IViewComponentResult> InvokeAsync() {

      return View("prlMessage");
    }
  }
}
