
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Commands.User {
  public class CreateUser : ICommand {

    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

  }
}
