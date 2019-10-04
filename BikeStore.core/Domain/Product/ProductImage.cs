using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.core.Domain.Product {
 public class ProductImage {

    [Key]
    public int IdxProductImage { get; set; }
    public int IdxProduct { get; set; }
    public string Name { get; set; }
    public long Size { get; set; }
    public byte[] Content { get; set; }

  }

}
