using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.Account {
  public class AccountService : IAccountService {

    IUserRepository mUserRepository;

    public AccountService(IUserRepository xUserRepository) {
      mUserRepository = xUserRepository;

    }
    public async Task<bool> CheckEmailIfAlreadyExistAsync(string xEmail) {

      User pUser = await mUserRepository.Get(xEmail);
      return pUser == null ? false : true;

    }
  }
}
