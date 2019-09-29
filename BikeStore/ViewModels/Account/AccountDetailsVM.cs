using BikeStore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.ViewModels.Account {
  public class AccountDetailsVM {

    public DateTime CreatedAt { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Username { get; set; }
    public int IdxUser { get; set; }


    public AccountDetailsVM() {

    }
    
  }
}
