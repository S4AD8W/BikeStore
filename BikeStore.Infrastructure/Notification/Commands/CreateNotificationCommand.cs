using BikeStore.core.Domain.Notification_NS;
using BikeStore.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Notification_NS.Commands {
  public class CreateNotificationCommand : ICommand {

    public string Dscr { get; set; }
    public string ForkModel { get; set; }
    public List<NotificationImage> Images { get; set; }
    public Guid UserId { get; set; }
    public string ForksModel { get; set; }
    public int IdxUser { get; set; }

  }
}
