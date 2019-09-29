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
  public class ChangeEmailHandler : ICommandHandler<ChangeEmailComman> {

    IUserService mUserService;
    IEmailService mEmailService;

    public ChangeEmailHandler(IUserService xUserService, IEmailService xEmailService) {
      mUserService = xUserService;
      mEmailService = xEmailService;
    }

    public async Task<CommandResult> HandleAsync(ChangeEmailComman xCommand) {

      CommandResult pResult = new CommandResult();

       if(!await mUserService.ChangeEmailAsync(xCommand.Email,xCommand.Password,xCommand.UserUuId)) {
        pResult.SetFailure(Languages.GetText(TextEnum.IncorrectPassword));
        return pResult;
      }

      pResult.SetSuccess();
      new Task(() => {
        //mEmailService.SendNewPasswordToUser(xCommand.Email, pNewPassword);
        var pMessage = this.GetEmailContent();
        mEmailService.SendEmail(xCommand.Email, pMessage.body, pMessage.Subiect);
      }).Start();

      return pResult;

    }


    private (string Subiect, string body) GetEmailContent() {

      //TODO:Utworzyć ciało wiadomość dla zmiany chasła
      return (Subiect: "Securiti Information", body: "Your Email is changed" + DateTime.Now.ToShortDateString());

    }

  }
}