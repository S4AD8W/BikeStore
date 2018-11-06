using BikeStore.Core;
using BikeStore.Core.Domain.Repository;
using BikeStore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace BikeStore.Infrastructure.Services
{
  public class UserService : IUserService {

    private readonly IUserRepository mUserRepository;

    public UserService(IUserRepository UserRepository) {

      mUserRepository = UserRepository;
    }

    public Task<UserDto> GetAsync(string email) {

      throw new NotImplementedException();
    }

    public Task LoginAsync(string email, string password) {
      throw new NotImplementedException();
    }


    public async Task RegisterAsync(Guid xUserId, string xEmail, string xUsername, string xPassword, string xName, string xSurname) {

      User pUser = await mUserRepository.GetAsync(xEmail);

      if (pUser == null) {
        throw new Exception("nosem janusza");
      }

      pUser = new User(xUserId, xEmail, xUsername, xPassword, xName, xUsername);

      await mUserRepository.AddAsync(pUser);

    }
  }
}
