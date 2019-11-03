using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.core.Domain {

  public enum OrderStatusEnum {
    New = 1,
    Prepared = 2,
    Realized = 3,
  }

  public enum DeleliveryMethodEenum {
    Courier = 1,
    InPost = 2,
    PersonalPickup =3
  }

  public enum PaymentMethodEenum {
    CashOnDelivery =1,
    Transfer = 2,
    PayU = 3,
  } 
  public class Order {



    [Key]
    public int IdxOrder { get; private set; }
    public Guid UuId { get; private set; }
    public int IdxUser { get; private set; }
    public OrderStatusEnum OrderStatus { get; private set; }
    public DeleliveryMethodEenum DeleliveryMethod { get; private set; }
    public PaymentMethodEenum PaymentMethod { get; private set; }
    public int IdxDeleliveryAddress { get; private set; }
    public bool IsInvoice { get; private set; } = false;
    public int IdxInvoiceAddress { get; private set; }
    public string OrderAttention { get; private set; }
    public bool IsAcceptStoreRules { get; private set; }
    public bool IsAcceptElectronicInvoice { get; private set; }
  }
}
