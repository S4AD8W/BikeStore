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
    public string ForksModel { get; set; }

    Collection<IFormFile> FormFile { get; set; }
  }
}
