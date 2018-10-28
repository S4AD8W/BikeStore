using BikeStore.Core;
using BikeStore.Core.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Repository
{
  public class UserRepository : IUserRepository
  {

    private static ISet<User> _users = new HashSet<User>{
      new User("janusz@gmail.com","password","nosacz","salt"),
      new User("janusz1@gmail.com","password","nosacz","salt"),
      new User("janusz2@gmail.com","password","nosacz","salt"),
    };
    public async Task AddAsync(User xUser)
    {
      _users.Add(xUser);
    }

    public async Task<User> GetAsync(Guid xId)
    => _users.SingleOrDefault(x => x.Id == xId);

    public async Task<User> GetAsync(string xEmail)
    => _users.SingleOrDefault(x => x.Email == xEmail.ToLowerInvariant());


    public async Task<IEnumerable<User>> GetAllAsync()
      => _users;


    public async Task RemoveAsync(Guid xId)
    {
      var pUser = await GetAsync(xId);
      _users.Remove(pUser);
    }

    public async Task UpdateAsync(User xUser)
    {

    }

  }

}