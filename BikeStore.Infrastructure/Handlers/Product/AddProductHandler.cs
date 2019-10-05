using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Product;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers.Product {
  public class AddProductHandler : ICommandHandler<AddProductCommand> {

    public Task<CommandResult> HandleAsync(AddProductCommand xCommand) {
      throw new NotImplementedException();
    }
  }
}
