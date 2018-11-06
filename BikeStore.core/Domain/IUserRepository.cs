using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Core.Domain.Repository
{
  public interface IUserRepository {

    Task<User> GetAsync(Guid id);
    Task<User> GetAsync(string xEmail);
    Task AddAsync(User xUser);
    Task<IEnumerable<User>> GetAllAsync();
    Task UpdateAsync(User xUser);
    Task RemoveAsync(Guid xId);
  }
}
