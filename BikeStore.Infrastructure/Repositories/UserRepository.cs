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
    
    public async Task<User> GetAsync(Guid xid) {

      var pUser = mBikeStoreContext.Users.FirstOrDefault(c => c.Id == xid);

      return pUser;

    }

    public async Task<User> GetAsync(string xEmail)
      => await Task.FromResult(mBikeStoreContext.Users.SingleOrDefault(x => x.Email.ToLower() == xEmail.ToLower()));

    public Task<IEnumerable<User>> GetAllAsync() {

      throw new NotImplementedException();

    }

    public async Task AddAsync(User xUser) {

      await mBikeStoreContext.AddAsync(xUser);
      mBikeStoreContext.SaveChanges();

    }

    public async Task UpdateAsync(User xUser)  {
     

      await mBikeStoreContext.SaveChangesAsync();

    }

    public async Task RemoveAsync(Guid xId) {

      var pUser =  await mBikeStoreContext.Users.FirstOrDefaultAsync(x => x.Id == xId);
      mBikeStoreContext.Users.Remove(pUser);
      await mBikeStoreContext.SaveChangesAsync();

    }

    public async Task<User> GetAsync(int xIdx) {

      return await mBikeStoreContext.Users.FirstOrDefaultAsync(x => x.IdxUser == xIdx);
    }
  }

}
