using BikeStore.core.Domain.Notification_NS;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.ViewModels.Notfication {
  public class ForkNotificationVM {
    [DisplayName("Opis")]
    public string Dscr { get; set; }
    [DisplayName("ModelAmrtyzatora")]
    public string ForkModel { get; set; }
    public IEnumerable<NotificationImage> NotificationImages { get; set; }


    public ForkNotificationVM(Notification xForknotification) {
      this.Dscr = xForknotification.Dscr;
      this.ForkModel = xForknotification.Model;
      
    }

    public ForkNotificationVM() {
        
    }

  }
}
