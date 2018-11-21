
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.DTO {
  public class cJwtDto {

    public string Token { get; set; }
    public long Expires { get; set; }
   
  }
}
