using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.ServiceNotfication;
using BikeStore.ViewModels.Notfication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  public class NotficationController : BikeStoreControllerBaseController {

    public NotficationController(ICommandDispatcher xCommandDispatcher)
           : base(xCommandDispatcher) {

    }

    public IActionResult Index() {
      return View();
    }
    [HttpGet]
    public IActionResult CreateForksNotfication() => View();

    [HttpPost]
    public async Task<IActionResult> CreateForksNotfication( ForkNotificationVM xNotification, ICollection<IFormFile> xfiles) {

      using (var memoryStream = new MemoryStream()) {
        //await  xProfileSystem.FileStream.CopyToAsync(memoryStream);
        //pProfileSystem.Image = memoryStream.ToArray();
      }
      //await DispatchAsync(xForkNotification);

      return RedirectToAction("CreateForksNotfication");

    }

  }
}