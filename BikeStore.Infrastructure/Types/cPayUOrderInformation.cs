using BikeStore.core.Domain;
using BikeStore.core.Domain.OrderNS;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Types {
  public class cPayUOrderInformation {
    public IEnumerable<OrderItem> OrderItems { get; set; }
    public string UserIp { get; set; }
    public string Description { get; set; }
    public string CurencCode { get; set; } = "PLN";
    public string BuyerEmail { get; set; }
    public string BuyerPhone { get; set; }
    public string BuyerFirstName { get; set; }
    public string BuyerLastName { get; set; }
    public string BuyerLanguage { get; set; } = "pl";
    public double Price { get; set; }
    public string NotifiUrl { get; set; }
 
  }
}