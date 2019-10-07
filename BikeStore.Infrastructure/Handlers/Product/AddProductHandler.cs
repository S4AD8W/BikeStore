using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Product;
using BikeStore.Infrastructure.Services.Product_NS;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers.Product {
  public class AddProductHandler : ICommandHandler<AddProductCommand> {

    private readonly IProductService mProductSerwice;

    public AddProductHandler(IProductService xProductSerwice) {
      mProductSerwice = xProductSerwice;
    }

    public async Task<CommandResult> HandleAsync(AddProductCommand xCommand) {

      CommandResult pResult = new CommandResult();

      if (await mProductSerwice.AddNewProductAsync(xCommand)) {
        pResult.SetSuccess(string.Empty);
      } else {
        pResult.SetFailure(string.Empty);
      }

      return pResult;
    }
  }
}
