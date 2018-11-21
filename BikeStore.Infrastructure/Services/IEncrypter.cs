using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Services {

 public interface IEncrypter {

    string GetSalt(string xValue);
    string GetHash(string xValue, string xSalt);

  }


}
