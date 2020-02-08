using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands {
 public class CreateOrderCommand : ICommand{

    public DeliveryMethodEenum DeliveryMethod { get; set; }
    public PaymentMethodEenum PaymentMethod { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public string AnotherName { get; set; }
    public string AnotherSurName { get; set; }
    public string AnotherEmail { get; set; }
    public string AnotherAddress { get; set; }
    public string AnotherPostCode { get; set; }
    public string AnotherCity { get; set; }
    public string AnotherPhoneNumber { get; set; }
    public bool DeleliveryAnotherAddress { get; set; }
    public bool AnotherDataInvoice { get; set; }
    public string Invoice_Name { get; set; }
    public string Invoice_Address { get; set; }
    public string Invoice_PostCode { get; set; }
    public string Invoice_Citi { get; set; }
    public bool IsOrderAttention { get; set; }
    public string Attention { get; set; }
    public bool IsEloctronicInvoice { get; set; }
    public bool IsAcceptStoreRules { get; set; }
    public DateTime CreateAt { get; set; }
    public string Ipv4 { get; private set; }
    public string Ipv6 { get; private set; }


    //public void SetConnectionData(string xIpv4, string xIpv6, int? xIdxUser, string xUserEmail) {
    //  CreateAt = DateTime.UtcNow;
    //  this.Ipv4 = xIpv4;
    //  this.Ipv6 = xIpv6;
    //  this.IdxUser = xIdxUser;
    //  this.UserEmail = xUserEmail;
    //}



    //public Address GetUserAddress()//funkcja zwracająca adres użytkownika
    //  => new Address(this.ZipCode, this.City, this.Street, this.HouseNumber, this.Phone, this.Country);

    //public Address GetDeliveryAddress() {
    //  //funkcja zwracająca adres dostawy/montarzu

    //  if (this.IsAccountDeliveryAdress)
    //    return new Address(this.ZipCode, this.City, this.Street, this.HouseNumber, this.Phone, this.Country);

    //  return new Address(this.ADZipCode, this.ADCity, this.ADStreet, this.ADHouseNumber, this.ADPhone, this.ADCountry);

    //}

    //public Address GetUserInvoiceAddress() {

    //  if (string.IsNullOrEmpty(this.IZipCode)) return null;
    //  if (string.IsNullOrEmpty(this.ICity)) return null;
    //  if (string.IsNullOrEmpty(this.IStreet)) return null;
    //  if (string.IsNullOrEmpty(this.IHouseNumber)) return null;
    //  if (string.IsNullOrEmpty(this.ICountry)) return null;

    //  //return new Address(this.IZipCode, this.ICity, this.IStreet, this.IHouseNumber, this.IPhone, this.ICountry);

    //}



  }

}
