using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Accounts {
  public class ResetPassword : ICommand {

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

  }
}
