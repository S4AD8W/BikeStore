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
      pSubiect = "BikeStore Potwierdz adres email";
      pBody = $"Prosze potwierdż swój adres email <a href='Http://{mHttpContextAccessor.HttpContext.Request.Host.Host}/Account/ConfirmEmail?xUid={xUserGuid}'>link</a> ";

      pIsSend = mEmailClient.SendEmail(pSubiect, pBody, xSendTo);

      return pIsSend;

    }

    public async Task SendNewPasswordToUser(string xSendTo, string xNewPassword) {
      //funkcja wysłająca email z nowym chasłem 

      string pMessage;
      string pSubiect;

      pSubiect = $@"Restart Password";
      pMessage = $@"Your new password to service:{xNewPassword}";

      mEmailClient.SendEmail(pSubiect, pMessage, xSendTo);  //wywołanie usługi aby wysłała nowe chasło

      await Task.CompletedTask;

    }

    public async Task SendForkTrackingId(string xSendTo, Guid xForkNotyficationGuid) {
      string pSubiect;
      string pBody;
      bool pIsSend;
      //TODO: Dodać nazwe kontlolera i akcję 
      pIsSend = false;
      pSubiect = "BikeStore nowe zgłoszenie";
      pBody = $"Twoje zgłoszenie zostało pomyślnie dodane status zgłoszenia możesz siledzić: <a href='Http://{mHttpContextAccessor.HttpContext.Request.Host.Host}/Notification/ForkNotyficationDetails?xUid={xForkNotyficationGuid}'>link</a> ";

      pIsSend = mEmailClient.SendEmail(pSubiect, pBody, xSendTo);

      
     
    }
  }
}
