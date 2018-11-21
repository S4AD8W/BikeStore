using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.core.Domain;
using BikeStore.Infrastructure.Commands.Users;
using BikeStore.Infrastructure.DTO;
using BikeStore.Infrastructure.Services.Emails;
using BikeStore.Infrastructure.Services.Messages;

namespace BikeStore.Infrastructure.Services {

  public class UserService : IUserService {

    private readonly IUserRepository mUserRepository;       //interfejs repozytorium użytkowników 
    private readonly IEncrypter mEncrypter;
    private readonly IMapper mMapper;
    private readonly IMessage mMessage;

    public UserService(IUserRepository xUserRepository, IEncrypter xEncrypter, IMapper xMapper, IMessage xMessage ) {

      mUserRepository = xUserRepository;
      mEncrypter = xEncrypter;
      mMapper = xMapper;
      mMessage = xMessage;

    }

    public async Task<cUserDto> GetUserAsync(string xEmail) {

      var pUser = await mUserRepository.Get(xEmail);

      return mMapper.Map<User,cUserDto >(pUser);

    } 

    public async Task <bool> LoginAsync(string xEmail, string xPassword) {

      User pUser = await mUserRepository.Get(xEmail);
      if(pUser == null) {
        return false;
      }

      string pHash = mEncrypter.GetHash(xPassword, pUser.Salt);

      if(pUser.Password == pHash) {
        return true;
      }

      return false;
      
    }

    public async Task<bool> RegisterAsync(Guid xUserId, string xEmail, string xUsername, string xPassword, string xRole) {

      User pUser = await mUserRepository.Get(xEmail);

      if (pUser != null) {
        mMessage.SetMesage("The user's e-mail address exists");
        return false;
      } else {
        string pSalt = mEncrypter.GetSalt(xPassword);
        string pHash = mEncrypter.GetHash(xPassword, pSalt);
        pUser = new User(xUserId, xEmail, xUsername, pHash, "rodo", "rodo", pSalt, xRole);
        await mUserRepository.Add(pUser);
        return true;
      }
      
    }

    
  }

}

