using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Users {
 public class LogIn : ICommand {

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string Password { get; set; }

  }
}
