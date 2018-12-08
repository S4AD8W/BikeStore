using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Domain;
using BikeStore.Ddlayer.entities;

namespace BikeStore.Infrastructure.Repositories {

  public class UserRepository : IUserRepository, ISqlRepository  {

    public readonly BikeStoreContext mBikeStoreContext;

    public UserRepository(BikeStoreContext xBikeStoreContext) {
      mBikeStoreContext = xBikeStoreContext;
    }

    private static readonly ISet<User> mUsers = new HashSet<User>{       //Przykładowe dane, w tym miejscu powina być warstwa dostepu do doanych 
            new User(new Guid(), "user3@email.com","secret","Jan", "Kwalski", "salt", "User" )
        };

    public async Task<User> Get(Guid id)
      => await Task.FromResult(mUsers.SingleOrDefault(x => x.Id == id));


    public async Task<User> Get(string xEmail)
      => await Task.FromResult(mUsers.SingleOrDefault(x => x.Email == xEmail));

    public Task<IEnumerable<User>> GetAll() {
      throw new NotImplementedException();
    }

    public async Task Add(User xUser) {

       await mBikeStoreContext.AddAsync(xUser);
      mBikeStoreContext.SaveChanges();
    }

    public async Task Update(User user) {
      await Task.CompletedTask;
    }

    public Task Remove(Guid id) {
      throw new NotImplementedException();
    }
  }

}
