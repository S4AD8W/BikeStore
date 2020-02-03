using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Types {
  public class cOAutchData {

    public string access_token { get; set; }
    public string token_type { get; set; }
    public string refresh_token { get; set; }
    public int expires_in { get; set; }
    public string grant_type { get; set; }
  }
}
