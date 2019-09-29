﻿using System;
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

    public UserService(IUserRepository xUserRepository, IEncrypter xEncrypter, IMapper xMapper, IMessage xMessage) {

      mUserRepository = xUserRepository;
      mEncrypter = xEncrypter;
      mMapper = xMapper;
      mMessage = xMessage;

    }


    private bool CheckValidPassord (string xPassword, string xSalt, string xPasswordHash) {

      string pHash = mEncrypter.GetHash(xPassword, xSalt);

     if(pHash != xPasswordHash) return false;

      return true;

    }

    public async Task<UserDTO> GetUserDTOAsync(string xEmail) {
      //funkcja zwracająca użytkownika
      //xEmail - email użytkownika 

      var pUser = await mUserRepository.GetAsync(xEmail);

      return mMapper.Map<User, UserDTO>(pUser);

    }

    public async Task<bool> LoginAsync(string xEmail, string xPassword) {
      //funkcja logująca użytkownika

      User pUser = await mUserRepository.GetAsync(xEmail);       //pobranie użytkownika z bazy

      if (pUser == null) {                                  //sprawdzenie czy użytkownik istnieje 
        return false;
      }

      string pHash = mEncrypter.GetHash(xPassword, pUser.Salt); //wygenerowanie Hasza z wprowadzonego chasła przez użytkownika

      if (pUser.Password == pHash) {                        //Porównanie z haszem z bazy danych czy się zgadza 
        pUser.UpdateAt();
        await mUserRepository.UpdateAsync(pUser);
        return true;
      }

      return false;

    }

    public async Task<bool> RegisterAsync(Guid xUserId, string xEmail, string xUsername, string xUserSurname, string xPassword, string xRole) {
      //funkcja rejestrująca nowego użytkownika

      User pUser = await mUserRepository.GetAsync(xEmail);       //pobranie użytkownika z bazy danych  

      if (pUser != null) {
        mMessage.SetMesage("The user's e-mail address exists"); // zwrucenie komunikatu 
        return false;
      } else {
        string pSalt = mEncrypter.GetSalt(xPassword);       //wygenerowanie soli dla hasła 
        string pHash = mEncrypter.GetHash(xPassword, pSalt);//wygenerowanie nowego hasła 
        pUser = new User(xUserId, xEmail, pHash, xUsername, xUserSurname, pSalt, xRole);
        await mUserRepository.AddAsync(pUser);                   //dodanie użytkownika do bazy danych 
        return true;
      }

    }

    public async Task<bool> ConfirmEmailAsync(Guid xUserId) {
      //funkcja potwierdzająca email 
      User pUser;

      pUser = await mUserRepository.GetAsync(xUserId);           // pobranie użytkownika z repozytorium 

      if (!pUser.IsEmailConfirm) {                          //sprawdzenie czy email jest już potwierdzony 
        pUser.IsEmailConfirm = true;
        await mUserRepository.UpdateAsync(pUser);                // zapis zaktualizowanego użytkownika
      } else {
        return false;
      }

      return true;

    }

    public async Task<string> ResetPassword(string xEmail) {
      //funkcja resetująca chasło 

      User pUser;
      string pNewPassword;
      UserDTO pUserDto;

      pUser = await mUserRepository.GetAsync(xEmail);

      if (pUser != null) {
        pUserDto = mMapper.Map<User, UserDTO>(pUser);

        pNewPassword = Guid.NewGuid().ToString("N").ToLower()
                       .Replace("1", "").Replace("o", "").Replace("0", "")
                       .Substring(0, 10);

        pUser.SetPassword(mEncrypter.GetHash(pNewPassword, pUser.Salt));
        await mUserRepository.UpdateAsync(pUser);

        return pNewPassword;
      }

      mMessage.SetMesage("This email doesn't exist");       //Komunikat błędu 

      return pNewPassword = null;                           //zwrucenie nulla jeżeli wystopił błąd 

    }

    public async Task<bool> CheckPasswordIsValidAsync(string xPassword, int xIdxUser) {

      User pUser = await mUserRepository.GetAsync(xIdxUser);

      if (pUser == null) return false;

      string pHash = mEncrypter.GetHash(xPassword, pUser.Salt);

      if (pHash != pUser.Password) return false;

      return true;

    }

    public async Task<bool> ChangePassword(string xNewPassword, Guid xUserUuId) {

      User pUser;
      string pNewPassword;
      

      pUser = await mUserRepository.GetAsync(xUserUuId);

      if (pUser == null) {
        return false;
      }

      pUser.SetPassword(mEncrypter.GetHash(xNewPassword, pUser.Salt));
        await mUserRepository.UpdateAsync(pUser);
        return true;


    }

    public async Task<bool> ChangeEmailAsync(string xEmail, string xPassword, Guid xUserUuid)  {

      User pUser = await mUserRepository.GetAsync(xUserUuid);

      if(!this.CheckValidPassord(xPassword, pUser.Salt,pUser.Password)) {
        return false;
      }

      pUser.SetEmail(xEmail);
      await mUserRepository.UpdateAsync(pUser);

      return true;

    }



    
  }

}

