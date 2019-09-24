using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Users {
  public class ChangePasswordCommand : ICommand {

    public string OldPassword { get; set; }
    public Guid UserUuid { get; set; }
  }
}
