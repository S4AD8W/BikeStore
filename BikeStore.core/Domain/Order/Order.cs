using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.core.Domain.OrderNS {
  public enum OrderStatusEnum {
    New = 1,
    Prepared = 2,
    Realized = 3,
    WaitingForPay = 4
  }

  public enum DeliveryMethodEenum {
    Courier = 1,
    InPost = 2,
    PersonalPickup = 3
  }

  public enum PaymentMethodEenum {
    CashOnDelivery = 1,
    Transfer = 2,
    PayU = 3,
  }
  
  public class Order {

    [Key]
    public int IdxOrder { get; private set; }
    public Guid UuId { get; private set; }
    public int IdxUser { get; private set; }
    public OrderStatusEnum OrderStatus { get; private set; }
    public DeliveryMethodEenum DeleliveryMethod { get; private set; }
    public PaymentMethodEenum PaymentMethod { get; private set; }
    public int IdxDeleliveryAddress { get; private set; }
    public bool IsInvoice { get; private set; } = false;
    public int IdxInvoiceAddress { get; private set; }
    public string OrderAttention { get; private set; }
    public bool IsAcceptStoreRules { get; private set; }
    public bool IsAcceptElectronicInvoice { get; private set; }
    public DateTime DateCreation { get; set; }
    public string Ipv4_Request { get; set; }
    public string Ipv6_Request { get; set; }

    public Order() {
        
    }
    public Order(string xIpv4_Request, string xIpv6_Request, int xIdxUser, int xIdxDeliveryAddress, 
      int xIdxInvoiceAddress) {

      this.Ipv4_Request = xIpv4_Request;
      this.Ipv6_Request = xIpv6_Request;
      this.IdxUser = xIdxUser;
      this.DateCreation = DateTime.UtcNow;
      this.IdxDeleliveryAddress = xIdxDeliveryAddress;
      this.IdxInvoiceAddress = xIdxInvoiceAddress;

    }
  }
}