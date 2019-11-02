using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.core.Domain {
 public class Address {
    
    public int IdxAddress { get; protected set; }
    public string ZipCode { get; protected set; }
    public string City { get; protected set; }
    public string Street { get; protected set; }
    public string HouseNumber { get; protected set; }
    public string Phone { get; protected set; }
    public string Country { get; protected set; }

  }
}
