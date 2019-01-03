using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Infrastructure.Commands;
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

  }
}