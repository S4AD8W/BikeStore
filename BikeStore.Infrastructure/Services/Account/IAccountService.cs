using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.Account {
public interface IAccountService : IService {

    Task<bool> CheckEmailIfAlreadyExistAsync(string xEmail);
  }
}
