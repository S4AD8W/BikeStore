using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Core
{
  
 public class User{

    public Guid Id { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public string Salt { get; protected set; }
    public string UserName { get; protected set; }
    public string FullName { get; protected set; }
    public DateTime CreateAt { get; protected set; }
    public DateTime UpdateAt { get; protected set; }

    protected User(){

    }

    public User(string xEmail, string xPassword, string xUserName, string xSalt){

      Id = new Guid();
      Email = xEmail.ToLowerInvariant();
      Password = xPassword;
      UserName = xUserName;
      Salt = xSalt;
      CreateAt = DateTime.Now;

    }

  }
}
