using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Users;
using BikeStore.Infrastructure.Services;
using BikeStore.Infrastructure.Services.Emails;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers.Users {
  public class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand> {

    IUserService mUserService;
    IEmailService mEmailService;
    public ChangePasswordHandler(IUserService xUserService) {
      mUserService = xUserService;
    }

    public async Task<CommandResult> HandleAsync(ChangePasswordCommand xCommand) {

      CommandResult pResult = new CommandResult();

      if(!await mUserService.ChangePassword(xCommand.OldPassword, xCommand.UserUuid)) {
        pResult.SetFailure();
      }

      new Task(() => {
        //mEmailService.SendNewPasswordToUser(xCommand.Email, pNewPassword);
        var pMessage = this.GetEmailContent();
        mEmailService.SendEmail(xCommand.xEmail, pMessage.body, pMessage.Subiect);
      }).Start();

      return pResult;

    }


    private (string Subiect, string body) GetEmailContent() {

      //TODO:Utworzyć ciało wiadomość dla zmiany chasła
      return (Subiect: "", body: "");

    }

  }
}
