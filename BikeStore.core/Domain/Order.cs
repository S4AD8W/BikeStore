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
public class Order {

    

    [Key]
    public int IdxOrder { get; private set; }
    public Guid UuId { get; private set; }
    public int IdxUser { get; private set; }
    public OrderStatusEnum OrderStatus { get; private set; }
    public int IdxDeleliveryAddress { get; private set; }
    public bool IsInvoice { get; private set; } = false;
    public int IdxInvoiceAddress { get; private set; }
    public string Orderattention { get; private set; }
    public bool IsAcceptStoreRules { get; private set; }
    public bool IsAcceptElectronicInvoice { get; private set; }
  }
}
