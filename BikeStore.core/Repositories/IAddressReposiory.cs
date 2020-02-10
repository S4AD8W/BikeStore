using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Repositories {
public interface IAddressReposiory : IRepository {
    IQueryable<Address> Addresses { get; }
    Task<Address> GetAsync(int xIdxAddress);
    Task DeleteAsync(int xIdxAddress);
    Task<int> AddAsync(Address xAddress);
    Task<int> Update(Address xAddress);
  }
}
