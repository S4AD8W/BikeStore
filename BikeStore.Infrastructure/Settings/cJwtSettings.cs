using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Settings {
public  class cJwtSettings {

    public string Key { get; set; }
    public string Issuer { get; set; }
    public int ExpiryMinutes { get; set; }

  }
}
