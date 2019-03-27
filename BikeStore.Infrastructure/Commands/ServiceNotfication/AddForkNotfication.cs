using BikeStore.core.Type;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands.ServiceNotfication {
 public class AddForkNotfication :ICommand {

    public string Dscr { get; set; }
    public string ForksModel { get; set; }
    public byte[] ForksImage { get; set; }
    List<ForkNotficationImage> Images { get; set; }
  }
}
