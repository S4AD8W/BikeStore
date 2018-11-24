using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Services.Messages {
 public class cMessage : IMessage {

    public bool IsMessage { get; set; } = false;
    public string Message { get; set; }
    public Guid UserId { get; set ; }

    public void SetMesage(string xMessage) {
      this.IsMessage = true;
      this.Message = xMessage;
    }
  }
}
