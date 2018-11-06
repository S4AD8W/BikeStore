using BikeStore.Core;
using BikeStore.Core.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Repository {
  public class UserRepository : IUserRepository {

    private static readonly ISet<User> mUsers = new HashSet<User>{       //Przykładowe dane, w tym miejscu powina być warstwa dostepu do doanych 
        new User(new Guid(), "user1@email.com", "user1", "secret","Jan", "Kwalski" ),
            new User( new Guid(), "user2@email.com", "user2", "secret", "Jan", "Kwalski" ),
            new User(new Guid(), "user3@email.com", "user3", "secret","Jan", "Kwalski" )
        };

    public async Task AddAsync(User xUser) {
      mUsers.Add(xUser);
      await Task.CompletedTask;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
      => await Task.FromResult(mUsers);

    public Task<User> GetAsync(Guid id) {
      throw new NotImplementedException();
    }

    public async Task<User> GetAsync(string xEmail)
      => await Task.FromResult(mUsers.SingleOrDefault(x => x.Email == xEmail.ToLowerInvariant()));


    public Task RemoveAsync(Guid id) {
      throw new NotImplementedException();
    }

    public Task UpdateAsync(User user) {
      throw new NotImplementedException();
    }
  }

}