using BikeStore.core.Domain;
using BikeStore.core.Domain.OrderNS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Order {
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
    public int IdxUser { get; set; }
    public string UserEmail { get; set; }


    public void SetConnectionData(string xIpv4, string xIpv6, int xIdxUser, string xUserEmail) {
      CreateAt = DateTime.UtcNow;
      this.Ipv4 = xIpv4;
      this.Ipv6 = xIpv6;
      this.IdxUser = xIdxUser;
      this.UserEmail = xUserEmail;
    }



    public Address GetUserAddress()//funkcja zwracająca adres użytkownika
      => new Address(this.PostCode,this.City,this.Address,this.Address,this.PhoneNumber, "PL");

    public Address GetDeliveryAddress() {
    //funkcja zwracająca adres dostawy/montarzu

      if (this.DeleliveryAnotherAddress)
        return new Address(this.AnotherPostCode,this.AnotherCity, this.AnotherAddress,this.AnotherAddress,this.AnotherPhoneNumber,"PL");

      return new Address(this.PostCode, this.City, this.Address, this.Address, this.PhoneNumber, "PL");

    }

    public Address GetUserInvoiceAddress() {

      if (string.IsNullOrEmpty(this.Invoice_Address)) return null;
      if (string.IsNullOrEmpty(this.Invoice_Citi)) return null;
      if (string.IsNullOrEmpty(this.Invoice_PostCode)) return null;

    return new Address(this.Invoice_PostCode,this.Invoice_Citi, this.Invoice_Address, this.Invoice_Address,"","PL");

    }



  }

}
  