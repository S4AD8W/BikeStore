using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Accounts;
using BikeStore.Infrastructure.Services;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers.Accounts {
  class ConfirmEmailHandler : ICommandHandler<ConfirmEmail_Command> {

    private readonly IUserService mUserService;

    public ConfirmEmailHandler(IUserService xUserService) {
      mUserService = xUserService;
    }

    public async Task<CommandResult> HandleAsync(ConfirmEmail_Command xCommand) {

      CommandResult pResult = new CommandResult();
      if(await mUserService.ConfirmEmailAsync(xCommand.UIdUser)) {
        pResult.SetSuccess(string.Empty);
      } else {
        pResult.SetFailure(string.Empty);
      }

      return pResult;
    }
  }
}
