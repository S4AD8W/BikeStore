using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseEntity {
  public class UserEntity {

    [Key]
    public int Idx { get; set; }
    public DateTime CreatedAt { get; protected set; }
    public string Email { get; protected set; }
    public Guid Id { get; protected set; }
    public bool IsEmailConfirm { get; set; }
    public string Name { get; protected set; }
    public string Password { get; protected set; }
    public string Role { get; set; }
    public string Salt { get; protected set; }
    public string Surname { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

  }
}
