using BikeStore.Infrastructure.Commands.Users;
using BikeStore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services{
 public interface IUserService : IService  {
    //interfejs serwisu użytkownika 

    Task<cUserDto> GetUserAsync(string xEmail);

    Task<bool> RegisterAsync(Guid xUserId, string xEmail,
            string xUserName, string xUserSurname, string xPassword, string xRole);

    Task<bool> LoginAsync(string xEmail, string xPassword);

    Task<bool> ConfirmEmail(Guid xUserId);

    Task<string> ResetPassword(string xEmail);

  }
}
