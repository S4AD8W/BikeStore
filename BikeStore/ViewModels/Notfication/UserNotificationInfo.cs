using BikeStore.core.Domain.Notification_NS;
using BikeStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.ViewModels.Notfication {
  public class UserNotificationInfo {

    public IEnumerable<Notification> Notification { get; set; }
    public PagingInfo PagingInfo { get; set; }


  }
}
