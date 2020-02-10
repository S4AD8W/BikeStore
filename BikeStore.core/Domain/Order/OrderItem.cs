using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BikeStore.core.Domain.OrderNS {
  public class OrderItem {
    

    [Key]
    public int IdxOrderItem { get; private set; }
    public int IdxOrder { get; private set; }
    public string Name { get; private set; }
    public int Quantiti { get; private set; }
    public double UnitPrice { get; private set; }
    public int IdxProduct { get; private set; }
    [NotMapped]
    public double TotalPrice => Quantiti * UnitPrice;

    public OrderItem(int idxOrder, string name, int quantiti, double unitPrice, int idxProduct) {
      IdxOrder = idxOrder;
      Name = name;
      Quantiti = quantiti;
      UnitPrice = unitPrice;
      IdxProduct = idxProduct;
    }
  }
}
