using Microsoft.AspNetCore.Http;
using BikeStore.Infrastructure.Services.Users;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.Emails {
 public class cEmailService : IEmailService {

    private readonly IEmailClient mEmailClient;             //Interfejs klienta mejlowego 
    private readonly IHttpContextAccessor  mHttpContextAccessor; //Kontekst aktualnego requesta http 

    public cEmailService (IEmailClient xEmailClient, IHttpContextAccessor xHttpContextAccessor) {
      mEmailClient = xEmailClient;
      mHttpContextAccessor = xHttpContextAccessor;
    }

    public bool SendUserAccountConfirmation(string xSendTo , Guid xUserGuid) {
      //funkcja wysyłająca link do potwierdzenia konta użytkownika 
      //xSendTo - adres na który ma być wysłane potwierdzenie
      //xUserGuid idetfikator użytkownia

      string pSubiect; 
      string pBody;
      bool pIsSend;

      pIsSend = false;
      pSubiect = "Oknowid_WebStore Potwierdz adres email";
      pBody = $"Prosze potwierdż swój adres email <a href='Http://{mHttpContextAccessor.HttpContext.Request.Host.Host}/{xUserGuid}'>link</a> ";

      pIsSend = mEmailClient.SendEmail(pSubiect, pBody, xSendTo);

      return pIsSend;

    }
  }
}
