using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  public class BikeStoreControllerBaseController : Controller {
                              
    //Klasa bazwa dla kontrolera implemtujaca powtarzające się zależności
    private readonly ICommandDispatcher CommandDispatcher;

  protected BikeStoreControllerBaseController(ICommandDispatcher xCommandDispatcher) {

    CommandDispatcher = xCommandDispatcher;

  }

  protected async Task DispatchAsync<T>(T command) where T : ICommand {

    await CommandDispatcher.DispatchAsync(command);

  }
}
}
