using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.Emails {

  public interface IEmailClient : IEmail {
    //intefejs obsługujący kliena pocztowego 

    bool SendEmail(string xSubiect, string xBody, string xSendTo);
    
  }
}
