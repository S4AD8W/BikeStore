using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  public class UserController : BikeStoreControllerBaseController{

    public UserController(ICommandDispatcher xCommandDispatcher, IMapper xMapper)
           : base(xCommandDispatcher, xMapper) {

    } 

    
    public IActionResult UserAccount() {
      return View();
    }

    public IActionResult ForkNotification() {

      return View();
    }
  }
}