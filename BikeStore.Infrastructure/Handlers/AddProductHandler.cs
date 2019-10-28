using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Cart;
using BikeStore.Infrastructure.Services.Cart;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers {
  public class AddProductHandler : ICommandHandler<AddProductCommand> {

    private readonly ICartService mCartService;
    public AddProductHandler(ICartService xCartService) {
      mCartService = xCartService;
    }

    public async Task<CommandResult> HandleAsync(AddProductCommand xCommand) {

      CommandResult pResult = new CommandResult();

      if(!await mCartService.AddProductAsync(xCommand)) {
        pResult.SetFailure();
      }

      pResult.SetSuccess();
      return pResult;
    }
  }
}
