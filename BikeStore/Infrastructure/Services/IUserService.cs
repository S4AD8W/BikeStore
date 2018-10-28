using BikeStore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services{
  
public interface IUserService {
  Task<UserDTO> GetAsync(string xEmail);
  Task RegisterAsync(string xEmail, string xUserName, string xPassword);
}

}