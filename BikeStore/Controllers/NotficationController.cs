using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Notification.Commands;
using BikeStore.ViewModels.Notfication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  public class NotficationController : BikeStoreControllerBaseController {

    public NotficationController(ICommandDispatcher xCommandDispatcher, IMapper xMapper)
           : base(xCommandDispatcher, xMapper) {

    }

    public IActionResult Index() {
      return View();
    }
    [HttpGet]
    public IActionResult CreateForksNotfication() => View();

    [HttpPost]
    public async Task<IActionResult> CreateForksNotfication(ForkNotificationVM xNotification, ICollection<IFormFile> xFiles) {

      CreateForkNotificationCommand pCommand;

      pCommand = mMapper.Map<ForkNotificationVM, CreateForkNotificationCommand>(xNotification);

      if (xFiles != null) {                                 //sprawdzenie czy lista plików niejest pusta i odczytanie zawatość 
        pCommand.Images = new List<core.Domain.Notification.ForkNotificationImage>();
        using (var memoryStream = new MemoryStream()) {
          foreach (var pFile in xFiles) {
            await pFile.CopyToAsync(memoryStream);
            pCommand.Images.Add(new core.Domain.Notification.ForkNotificationImage {
              Content = memoryStream.ToArray(),
              Name = pFile.Name,
              Size = pFile.Length
            });
          }
        }
      }

      await DispatchAsync(pCommand);

      return RedirectToAction("CreateForksNotfication");

    }

  }
}