using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Types {

  public class CommandResult {
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

    public void SetSuccess(string xMessage) {
      this.IsSuccess = true;
      this.Message = xMessage;
    }

    public void SetFailure(string xMessage) {
      this.IsSuccess = false;
      this.Message = xMessage;
    }

  }
}

  
