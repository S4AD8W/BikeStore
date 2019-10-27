using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Cart {
public class AddProductCommand : BikeStore.Infrastructure.Commands.ICommand {

    public int IdxProduct { get; set; }
    public Guid UuIdUser { get; set; }
    public int Quantity { get; set; }
  }
}
