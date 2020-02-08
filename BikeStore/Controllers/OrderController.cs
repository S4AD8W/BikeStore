using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Dispatcher;
using BikeStore.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  public class OrderController : BikeStoreControllerBaseController {
   

    public OrderController(IDispatcher xCommandDispatcher, IMapper xMapper)
           : base(xCommandDispatcher, xMapper) {
    }

    public IActionResult Detail() {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(DetailOrderVM xDetailOrdor) {

      return View();
    }

  }
}
