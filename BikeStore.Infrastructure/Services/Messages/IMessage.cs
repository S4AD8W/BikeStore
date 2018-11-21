using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Services.Messages {
  public interface IMessage : IService {

    bool IsMessage { get; set; }
    string Message { get; set; }

    void SetMesage(string xMessage);

  }
}
