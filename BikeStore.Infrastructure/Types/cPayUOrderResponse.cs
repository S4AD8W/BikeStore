using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Types {
  public class cPayUOrderResponse {

    public class StatusResponse {
      public string StatusCode { get; set; }
      public string statusDesc { get; set; }
    }

    public StatusResponse Status { get; set; }
    public string RedirectUri { get; set; }
    public string OrderId { get; set; }
    public string ExtOrderId { get; set; }
  }
}


