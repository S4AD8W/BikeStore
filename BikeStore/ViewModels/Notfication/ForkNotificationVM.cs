using BikeStore.core.Domain.Notification;
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
    public IEnumerable<ForkNotificationImage> ForkNotificationImages { get; set; }


    public ForkNotificationVM(ForkNotification xForknotification) {
      this.Dscr = xForknotification.Dscr;
      this.ForkModel = xForknotification.ForksModel;
      
    }

    public ForkNotificationVM() {
        
    }

  }
}
