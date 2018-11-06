using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Core {
  public class User {

    public Guid Id { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public string Username { get; protected set; }
    public string Name { get; protected set; }
    public string Surname { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }


    protected User() {

    }

    public User(Guid xUserId, string xEmail, string xUsername, string xPassword, string xName, string xSurname) {
      Id = xUserId;
      SetEmail(xEmail);
      SetUsername(xUsername);
      SetPassword(xPassword);
      CreatedAt = DateTime.UtcNow;
      SetName(xName);
      SetSurname(xSurname);
    }


    public void SetUsername(string xUsername) {

      Username = xUsername.ToLowerInvariant();
      UpdatedAt = DateTime.UtcNow;
    }

    public void SetEmail(string xEmail) {

      if (string.IsNullOrWhiteSpace(xEmail)) {
        throw new Exception();
      }

      if (Email == xEmail) {
        return;
      }

      Email = xEmail.ToLowerInvariant();
      UpdatedAt = DateTime.UtcNow;
    }


    public void SetPassword(string xPassword) {
      if (string.IsNullOrWhiteSpace(xPassword)) {
        throw new Exception();
      }
      if (xPassword.Length < 4) {
        throw new Exception();
      }
      if (xPassword.Length > 100) {
        throw new Exception();
      }
      if (Password == xPassword) {
        return;
      }
      Password = xPassword;
      UpdatedAt = DateTime.UtcNow;
    }

    public void SetName(string xName) {
      if (string.IsNullOrWhiteSpace(xName)) {
        return;
      }

      Name = xName;
      UpdatedAt = DateTime.UtcNow;
    }

    public void SetSurname(string xSurname) {

      if (string.IsNullOrWhiteSpace(xSurname)) {

        return;
      }

      Surname = xSurname;
      UpdatedAt = DateTime.UtcNow;

    }
  }
}
