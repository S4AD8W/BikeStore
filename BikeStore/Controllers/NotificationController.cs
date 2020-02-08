using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.core.Domain.Notification_NS;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Dispatcher;
using BikeStore.Infrastructure.EF;
using BikeStore.Infrastructure.Notification_NS.Commands;
using BikeStore.ViewModels.Notfication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Controllers {
  public class NotificationController : BikeStoreControllerBaseController {

    private readonly BikeStoreContext mDB;

    public NotificationController(IDispatcher xCommandDispatcher, IMapper xMapper, BikeStoreContext xDB)
           : base(xCommandDispatcher, xMapper) {
      mDB = xDB;
    }

    public IActionResult Index() {
      return View();
    }
    [HttpGet]
    public IActionResult CreateForksNotfication() => View();

    [HttpPost]
    public async Task<IActionResult> CreateForksNotfication(ForkNotificationVM xNotification, ICollection<IFormFile> xFiles) {

      CreateNotificationCommand pCommand;

      pCommand = mMapper.Map<ForkNotificationVM, CreateNotificationCommand>(xNotification);

      if (xFiles != null) {                                 //sprawdzenie czy lista plików niejest pusta i odczytanie zawatość 
        pCommand.Images = new List<core.Domain.Notification_NS.NotificationImage>();
        using (var memoryStream = new MemoryStream()) {
          foreach (var pFile in xFiles) {
            await pFile.CopyToAsync(memoryStream);
            pCommand.Images.Add(new core.Domain.Notification_NS.NotificationImage {
              Content = memoryStream.ToArray(),
              Name = pFile.FileName,
              Size = pFile.Length
            });
          }
        }
      }

      await SendAsync(pCommand);
      if (CommandResult.IsSuccess) {
        return RedirectToAction("Index", "Home");
      }
      return RedirectToAction("CreateForksNotfication");

    }

    public async Task<IActionResult> NotificationDetails(Guid xUid) {

      Notification pNotification = mDB.Notifications.SingleOrDefault(x => x.Guid == xUid);
      ForkNotificationVM pVM = new ForkNotificationVM(pNotification);

      pVM.NotificationImages = await mDB.NotificationImages.Where(x => x.IdxNotification == pNotification.IdxNotification).ToListAsync();



      return View(pVM);

    }
  }
}