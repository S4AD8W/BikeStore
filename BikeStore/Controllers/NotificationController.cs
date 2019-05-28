using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.core.Domain.Notification;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.EF;
using BikeStore.Infrastructure.Notification.Commands;
using BikeStore.ViewModels.Notfication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Controllers {
  public class NotificationController : BikeStoreControllerBaseController {

    private readonly BikeStoreContext mDB;

    public NotificationController(ICommandDispatcher xCommandDispatcher, IMapper xMapper, BikeStoreContext xDB)
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

      CreateForkNotificationCommand pCommand;

      pCommand = mMapper.Map<ForkNotificationVM, CreateForkNotificationCommand>(xNotification);

      if (xFiles != null) {                                 //sprawdzenie czy lista plików niejest pusta i odczytanie zawatość 
        pCommand.Images = new List<core.Domain.Notification.ForkNotificationImage>();
        using (var memoryStream = new MemoryStream()) {
          foreach (var pFile in xFiles) {
            await pFile.CopyToAsync(memoryStream);
            pCommand.Images.Add(new core.Domain.Notification.ForkNotificationImage {
              Content = memoryStream.ToArray(),
              Name = pFile.FileName,
              Size = pFile.Length
            });
          }
        }
      }

      await DispatchAsync(pCommand);
      if (CommandResult.IsSuccess) {
        return RedirectToAction("Index", "Home");
      }
      return RedirectToAction("CreateForksNotfication");

    }

    public async Task<IActionResult> ForkNotyficationDetails(Guid xUid) {

      ForkNotification pForkNotification = mDB.ForksNotifications.SingleOrDefault(x => x.Guid == xUid);
      ForkNotificationVM pVM = new ForkNotificationVM(pForkNotification);

      pVM.ForkNotificationImages = await mDB.ForkNotficationImages.Where(x => x.IdxForkNotfication == pForkNotification.IdxForkNotfication).ToListAsync();



      return View(pVM);

    }
  }
}