using BikeStore.core.Domain.Notification;
using BikeStore.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Areas.Admin.ViewModel {
  public class AllForkVM {

    public IEnumerable<ForkNotification> Forks { get; set; }
    public PagingInfo PagingInfo { get; set; }

  }
}
