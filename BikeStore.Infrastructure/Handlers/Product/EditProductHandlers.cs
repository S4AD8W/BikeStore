using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Product;
using BikeStore.Infrastructure.Services.Product_NS;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers.Product {
  public class EditProductHandler : ICommandHandler<EditProductCommand> {

    public readonly IProductService mProductService;

    public EditProductHandler(IProductService xProductService) {
      mProductService = xProductService;
    }
    public async Task<CommandResult> HandleAsync(EditProductCommand xCommand) {

      CommandResult pResult = new CommandResult();

      if (!await mProductService.EditProductAsync(xCommand)) {
        pResult.SetFailure();
        return pResult;
      }

      pResult.SetSuccess();

      return pResult;
    }
  }
}
