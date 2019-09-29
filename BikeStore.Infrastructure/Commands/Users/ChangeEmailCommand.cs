using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Users {
  public class ChangeEmailComman: ICommand {

    public string Email { get; set; }
    public string Password { get; set; }
    public Guid UserUuId { get; set; }
  }
}
