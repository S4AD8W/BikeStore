using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Users;
using BikeStore.Infrastructure.Services;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers.Users {
  public class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand> {

    IUserService mUserService;

    public ChangePasswordHandler(IUserService xUserService) {
      mUserService = xUserService;
    }

    public async Task<CommandResult> HandleAsync(ChangePasswordCommand xCommand) {

      CommandResult pResult = new CommandResult();

      return pResult;

    }
}
