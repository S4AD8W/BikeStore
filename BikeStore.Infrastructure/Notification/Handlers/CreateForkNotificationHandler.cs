using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Notification.Commands;
using BikeStore.Infrastructure.Notification.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Notification.Handlers {
 public class CreateForkNotificationHandler : ICommandHandler<CreateForkNotificationCommand> {

    private readonly IForkNotificationService mForkNotyificationServices;

    public CreateForkNotificationHandler(IForkNotificationService xForkNotificationService) {
      mForkNotyificationServices = xForkNotificationService;
    }

    public async Task HandleAsync(CreateForkNotificationCommand xCommand) {

      await mForkNotyificationServices.AddForkNotificationAsync(xCommand);
    }


  }
}
