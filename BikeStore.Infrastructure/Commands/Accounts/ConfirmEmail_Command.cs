using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Accounts {
  public class ConfirmEmail_Command : BikeStore.Infrastructure.Commands.ICommand {

    public Guid UIdUser { get; set; }

    public ConfirmEmail_Command(Guid xUIdUser) {
      this.UIdUser = xUIdUser;

    }

  }
}
