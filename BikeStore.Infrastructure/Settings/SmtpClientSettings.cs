using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Settings {
  public class SmtpClientSettings {

    public string EmailAddres { get; set; }
    public string EmailPassword { get; set; }
    public string SmtpAddress { get; set; }
    public int SmtpPort { get; set; }
    public bool UseSsl { get; set; }
  }
}
