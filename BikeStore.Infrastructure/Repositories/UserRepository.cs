using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.core.Domain;
using BikeStore.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Infrastructure.Repositories {

  public class UserRepository : IUserRepository, ISqlRepository {

    public readonly BikeStoreContext mBikeStoreContext;
    public readonly IMapper mMapper;

    public UserRepository(BikeStoreContext xBikeStoreContext, IMapper xMapper) {
      mBikeStoreContext = xBikeStoreContext;
      mMapper = xMapper;
    }

    private static readonly ISet<User> mUsers = new HashSet<User>{       //Przykładowe dane, w tym miejscu powina być warstwa dostepu do doanych 
            new User(new Guid(), "user3@email.com","secret","Jan", "Kwalski", "salt", "User" )
        };

    public async Task<User> Get(Guid xid) {

      var pUser = mBikeStoreContext.Users.SingleOrDefault(c => c.Id == xid);

      return pUser;

    }



    public async Task<User> Get(string xEmail)
      => await Task.FromResult(mBikeStoreContext.Users.SingleOrDefault(x => x.Email == xEmail.ToLower()));

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
