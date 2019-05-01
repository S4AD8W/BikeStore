using BikeStore.core.Domain.Notification;
using BikeStore.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Notification.Commands {
  public class CreateForkNotificationCommand : ICommand {

    public string Dscr { get; set; }
    public string ForkModel { get; set; }
    public List<ForkNotificationImage> Images { get; set; }
  }
}
