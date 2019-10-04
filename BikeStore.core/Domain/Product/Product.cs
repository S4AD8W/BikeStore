using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.core.Domain.Product {
public class Product {

    [Key]
    public int IdxProduct { get; private set; }
    public string Name { get; private set; }
    public string Descryption { get; private set; }
    public decimal Price { get; private set; }
    public int  IdxCategory { get; private set; }
    public DateTime CreateAt { get; private set; }
    public DateTime EditAt { get; private set; }

  }
}
