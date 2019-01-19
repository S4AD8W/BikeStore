using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.ViewModels {
  public class CartIndexVM {
    public Cart Cart { get; set; }
    public string ReturnUrl { get; set; }
  }
}
