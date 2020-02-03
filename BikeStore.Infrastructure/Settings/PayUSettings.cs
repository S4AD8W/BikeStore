using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Settings {
  public class PayUSettings {
    public string Post_Id { get; set; }
    public string Md5 { get; set; }
    public string Client_Id { get; set; }
    public string Client_Secret { get; set; }
    public string OAuth_Uri { get; set; }
    public string ApiUri { get; set; }

  }
}
