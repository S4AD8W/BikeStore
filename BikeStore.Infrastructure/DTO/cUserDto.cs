using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.DTO{

 public class cUserDto  {

    public string Email { get; set; }
    public string FullName { get; set; }
    public string Name { get; set; }
    public string Role { get; set; } 
    public string Surname { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; }

  }

}
