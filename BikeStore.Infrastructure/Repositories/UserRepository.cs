using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Domain;

namespace BikeStore.Infrastructure.Repositories {

  public class UserRepository : IUserRepository {

    private static readonly ISet<User> mUsers = new HashSet<User>{       //Przykładowe dane, w tym miejscu powina być warstwa dostepu do doanych 
            new User(new Guid(), "user3@email.com", "user3", "secret","Jan", "Kwalski", "salt", "User" )
        };

    public async Task<User> Get(Guid id) {
      throw new NotImplementedException();
    }

    public async Task<User> Get(string xEmail)
      => await Task.FromResult(mUsers.SingleOrDefault(x => x.Email == xEmail));
      
    public Task<IEnumerable<User>> GetAll() {
      throw new NotImplementedException();
    }

    public async Task Add(User xUser) {

      mUsers.Add(xUser);
      await Task.CompletedTask;

    }

    public Task Update(User user) {
      throw new NotImplementedException();
    }

    public Task Remove(Guid id) {
      throw new NotImplementedException();
    }
  }

}
