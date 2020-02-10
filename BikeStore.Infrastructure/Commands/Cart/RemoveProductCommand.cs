using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Commands.Cart {
public class RemoveProductCommand : BikeStore.Infrastructure.Commands.ICommand {

    public int IdxProduct { get; set; }
  }
}
