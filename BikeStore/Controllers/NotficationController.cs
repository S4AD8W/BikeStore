using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.ServiceNotfication;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  public class NotficationController : BikeStoreControllerBaseController {

    public NotficationController(ICommandDispatcher xCommandDispatcher )
           : base(xCommandDispatcher) {
      
    }

    public IActionResult Index() {
      return View();
    }
    [HttpGet]
    public IActionResult CreateForksNotfication() =>  View();

    [HttpPost]
    public async Task<IActionResult> CreateForksNotfication(AddForkNotfication xForkNotification) {

      await DispatchAsync(xForkNotification);

      return RedirectToAction("CreateForksNotfication");

    }

  }
}