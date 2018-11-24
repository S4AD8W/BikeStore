using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.DTO{

 public class cUserDto  {

    public DateTime CreatedAt { get; set; }
    public string Email { get; set; }
    public Guid Id { get; set; }
    public bool IsEmailConfirm { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Salt { get; set; }
    public string Surname { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Username { get; set; }

  }

}
