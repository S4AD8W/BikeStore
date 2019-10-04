using BikeStore.core.Domain.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.core.Domain {
 

  public class User {
    [Key]
    public int IdxUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Email { get; private set; }
    public Guid Id { get; private set; }
    public bool IsEmailConfirm { get; set; }
    public string Name { get; private set; }
    public string Password { get; private set; }
    public string Role { get; set; }
    public string Salt { get; private set; }
    public string Surname { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public ICollection<ForkNotification> ForkNotifications { get; set; }
   

    protected User() {

    }

    public User(Guid xUserId, string xEmail, string xPassword, string xName, string xSurname, string xSalt, string xRole) {
      Id = xUserId;
      SetEmail(xEmail);
      SetPassword(xPassword);
      CreatedAt = DateTime.UtcNow;
      SetName(xName);
      SetSurname(xSurname);
      SetSatl(xSalt);
      this.Role = xRole;
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
      //if (string.IsNullOrWhiteSpace(xPassword)) {
      //  throw new Exception();
      //}
      //if (xPassword.Length < 4) {
      //  throw new Exception();
      //}
      //if (xPassword.Length > 100) {
      //  throw new Exception();
      //}
      //if (Password == xPassword) {
      //  return;
      //}
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

    public void SetSatl(string xSalt) {

      this.Salt = xSalt;

    }

    public void UpdateAt() {
      UpdatedAt = DateTime.UtcNow;
    }

  }
}
