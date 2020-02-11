using BikeStore.Infrastructure.Services.Emails;
using BikeStore.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace BikeStore.Infrastructure.Services.Users {
  public class cEmailClient : IEmailClient  {

    private readonly SmtpClientSettings mSmtpClientSettings;
    private bool mIsSent = false;
    
    public cEmailClient (SmtpClientSettings xSmtpClientSettings) {
      //xSmtpClientSettings obiekt zkonfiguracją 

      mSmtpClientSettings = xSmtpClientSettings;            //wstrzykniecie konfiguracji smtp klienta

    }
   
    public bool SendEmail(string xSubject, string xBody, string xSendTo) {
      //funkcja wysłająca meile 
      //xSubiect - temat wiadomość 
      //xBody - ciało wiadomość
      //xSendTo - adres na jaki ma zostać wysłana wiadomość 

      SmtpClient pSmtpClient;
      MailMessage pMailMessage;
      bool pIsSent;

      pSmtpClient= GetSmtpClient();
      pMailMessage = new MailMessage(mSmtpClientSettings.EmailAddres, xSendTo);
      pMailMessage.IsBodyHtml = true;
      pMailMessage.Body = xBody;
      pMailMessage.Subject = xSubject;

      pIsSent = false;
      try {

      //pSmtpClient.SendCompleted += new
      //      SendCompletedEventHandler(SendCompletedCallback);
       pSmtpClient.Send(pMailMessage );
      } catch (Exception ex) {
        mIsSent = false;
        
      }

      pSmtpClient.Dispose();

      return mIsSent;
    }

    private SmtpClient GetSmtpClient() {

      SmtpClient pSmtpClient = new SmtpClient(mSmtpClientSettings.SmtpAddress,mSmtpClientSettings.SmtpPort);
      
      pSmtpClient.Credentials = new NetworkCredential(mSmtpClientSettings.EmailAddres, mSmtpClientSettings.EmailPassword);
      pSmtpClient.EnableSsl = mSmtpClientSettings.UseSsl;
      
      return pSmtpClient;
    }

    private  void SendCompletedCallback(object sender, AsyncCompletedEventArgs e) {
      // Get the unique identifier for this asynchronous operation.

      
     

      if (e.Cancelled) {

        mIsSent = false;
      }

      if (e.Error != null) {

        mIsSent = false;

      } else {
        


      }

      mIsSent = false;
    }

  }
}
