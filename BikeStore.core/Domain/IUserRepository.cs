using BikeStore.core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Domain {
  public interface IUserRepository : IRepository {

    Task<User> GetAsync(Guid xId);
    Task<User> GetAsync(string xEmail);
    Task<User> GetAsync(int xIdx);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task RemoveAsync(Guid id);

  }
}
