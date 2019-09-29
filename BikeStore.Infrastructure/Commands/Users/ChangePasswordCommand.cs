using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Users {
  public class ChangePasswordCommand : ICommand {

    public string OldPassowrd { get; set; }
    public Guid UserUuid { get; set; }
    public string xEmail { get; set; }
  }
}
