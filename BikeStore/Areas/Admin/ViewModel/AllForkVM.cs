using BikeStore.core.Domain.Notification_NS;
using BikeStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Areas.Admin.ViewModel {
  public class AllForkVM {

    public IEnumerable<Notification> Notifications { get; set; }
    public PagingInfo PagingInfo { get; set; }

  }
}
