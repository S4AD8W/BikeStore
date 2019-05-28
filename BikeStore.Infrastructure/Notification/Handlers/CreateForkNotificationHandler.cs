using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Notification.Commands;
using BikeStore.Infrastructure.Notification.Services;
using BikeStore.Infrastructure.Services.Emails;
using BikeStore.Infrastructure.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using BikeStore.Infrastructure.Extensions;

namespace BikeStore.Infrastructure.Notification.Handlers {
  public class CreateForkNotificationHandler : ICommandHandler<CreateForkNotificationCommand> {

    private readonly IForkNotificationService mForkNotyificationServices;
    private readonly IEmailService mEmailService;
    private readonly IHttpContextAccessor mHttpContextAcessor;
    public CreateForkNotificationHandler(IForkNotificationService xForkNotificationService, IEmailService xEmailService, IHttpContextAccessor xHttpContextAcessor) {
      mForkNotyificationServices = xForkNotificationService;
      mEmailService = xEmailService;
      mHttpContextAcessor = xHttpContextAcessor;
    }

    public async Task<CommandResult> HandleAsync(CreateForkNotificationCommand xCommand) {

      CommandResult pCommandResult = new CommandResult();
      Guid pUseruId;
      

      Guid.TryParse(mHttpContextAcessor.HttpContext.User.Identity.Name, out pUseruId);
      var pIdxUser = mHttpContextAcessor.HttpContext.User.Claims.Where(x => x.Type == CustomClaim.IdxUser)
              .Select(x => x.Value).SingleOrDefault();

      xCommand.IdxUser = Convert.ToInt32(pIdxUser);
      xCommand.UserId = pUseruId;
      var pResul = await mForkNotyificationServices.AddForkNotificationAsync(xCommand);

      if (pResul.IsSucces) {
        string pEmail = mHttpContextAcessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Email)
                                     .Select(x => x.Value).SingleOrDefault();
        new Task(() => {
          mEmailService.SendForkTrackingId(pEmail, pResul.ForkNotification_Guid);
        }).Start();

        pCommandResult.SetSuccess(String.Empty);
      }


      return pCommandResult;

    }
  }
}


