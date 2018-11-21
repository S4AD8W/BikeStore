using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Accounts {
 public class cLogin : ICommand {

    public Guid TokenId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

  }

}
