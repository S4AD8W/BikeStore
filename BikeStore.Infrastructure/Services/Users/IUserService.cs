using BikeStore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services{

  public interface IUserService : IService {

    Task<UserDto> GetAsync(string email);
    Task RegisterAsync(Guid userId, string email,
        string username, string password, string xName, string xSurname);
    Task LoginAsync(string email, string password);
  }
}