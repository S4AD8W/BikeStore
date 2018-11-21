using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.Emails {
 public interface IEmailService : IService {
    //intefejs usługi mejlowej 

    bool SendUserAccountConfirmation(  string xSendTo, Guid xUserGuid);

  }
}
