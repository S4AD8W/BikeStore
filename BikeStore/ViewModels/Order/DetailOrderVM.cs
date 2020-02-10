using BikeStore.core.Domain;
using BikeStore.core.Domain.OrderNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.ViewModels.Order {
  public class DetailOrderVM {

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

  }
}
