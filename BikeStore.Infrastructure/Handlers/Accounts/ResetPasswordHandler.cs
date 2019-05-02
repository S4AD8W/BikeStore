using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Accounts;
using BikeStore.Infrastructure.Services;
using BikeStore.Infrastructure.Services.Emails;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers.Accounts {
  class ResetPasswordHandler : ICommandHandler<ResetPassword> {

    private readonly IUserService mUserService;
    private readonly IEmailService mEmailService;

    public ResetPasswordHandler(IUserService xUserService, IEmailService xEmailService) {
      mUserService = xUserService;
      mEmailService = xEmailService;
    }

    public async Task<CommandResult> HandleAsync(ResetPassword xCommand) {

      string pNewPassword;

      pNewPassword = await mUserService.ResetPassword(xCommand.Email); //wywołanie serwisu aby zmienić chasło 
                                                                       
      if (pNewPassword != null) {                           //Sprawdzenie czy serwis zmienił hasło 

        new Task(() => {                                  //utworzenie nowego wontku aby wysłać emaila z nowym hasłem 
          Thread.CurrentThread.IsBackground = true;
          mEmailService.SendNewPasswordToUser(xCommand.Email, pNewPassword); //wywołanie serwisu aby wysłać email 
        }).Start();

      }
      return new CommandResult();
    }

  }
}
