using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands {
 public class CreateOrderCommand : ICommand{

    public string ADCity { get; set; }
    public string ADCountry { get; set; }
    public string ADFirstName { get; set; }
    public string ADHouseNumber { get; set; }
    public string ADLastName { get; set; }
    public string ADPhone { get; set; }
    public string ADStreet { get; set; }
    public string ADZipCode { get; set; }
    public string City { get; set; }
    public PaymentMethod CntPayMethod { get; set; }
    public string Country { get; set; }
    public DateTime CreateAt { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string HouseNumber { get; set; }
    public string ICity { get; set; }
    public string ICountry { get; set; }
    public int? IdxUser { get; private set; }
    public string IFirstName { get; set; }
    public string IHouseNumber { get; set; }
    public string ILastName { get; set; }
    public string IPhone { get; set; }
    public string Ipv4 { get; private set; }
    public string Ipv6 { get; private set; }
    public bool IsAccountDeliveryAdress { get; set; } = false;
    public string IStreet { get; set; }
    public string IZipCode { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Street { get; set; }
    public string UserEmail { get; private set; }
    public string ZipCode { get; set; }

    public void SetConnectionData(string xIpv4, string xIpv6, int? xIdxUser, string xUserEmail) {
      CreateAt = DateTime.UtcNow;
      this.Ipv4 = xIpv4;
      this.Ipv6 = xIpv6;
      this.IdxUser = xIdxUser;
      this.UserEmail = xUserEmail;
    }



    public Address GetUserAddress()//funkcja zwracająca adres użytkownika
      => new Address(this.ZipCode, this.City, this.Street, this.HouseNumber, this.Phone, this.Country);

    public Address GetDeliveryAddress() {
      //funkcja zwracająca adres dostawy/montarzu

      if (this.IsAccountDeliveryAdress)
        return new Address(this.ZipCode, this.City, this.Street, this.HouseNumber, this.Phone, this.Country);

      return new Address(this.ADZipCode, this.ADCity, this.ADStreet, this.ADHouseNumber, this.ADPhone, this.ADCountry);

    }

    public Address GetUserInvoiceAddress() {

      if (string.IsNullOrEmpty(this.IZipCode)) return null;
      if (string.IsNullOrEmpty(this.ICity)) return null;
      if (string.IsNullOrEmpty(this.IStreet)) return null;
      if (string.IsNullOrEmpty(this.IHouseNumber)) return null;
      if (string.IsNullOrEmpty(this.ICountry)) return null;

      return new Address(this.IZipCode, this.ICity, this.IStreet, this.IHouseNumber, this.IPhone, this.ICountry);

    }



  }

}
