using BikeStore.core.Domain;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Repositories {
  public class AddressRepository : IAddressReposiory {
    
    private readonly BikeStoreContext mDbContext;
    public IQueryable<Address> Addresses => mDbContext.Addresses;
    

    public AddressRepository(BikeStoreContext xDbContext) {

      mDbContext = xDbContext;
      
    }


    public async Task DeleteAsync(int xIdxAddress) {
      //funkcja usówająca adres z bazy danych
      //xIdxAddress

      Address pAddress;

      pAddress = await mDbContext.Addresses.FirstOrDefaultAsync(pAddressx => pAddressx.IdxAddress == xIdxAddress);
      mDbContext.Remove(pAddress);
      await mDbContext.SaveChangesAsync();

    }

    public async Task<Address> GetAsync(int xIdxAddress)
      //funkcja zwracająca adres po indeksie adresu
      //xIdxAddress - indeks adresu 
      => await mDbContext.Addresses.FirstOrDefaultAsync(pAddress => pAddress.IdxAddress == xIdxAddress);

    public async Task<int> AddAsync(Address xAddress) {
      //funkcja dodająca adres do bazy danych
      //xAddress - adres do dodania

      await mDbContext.Addresses.AddAsync(xAddress);
      await mDbContext.SaveChangesAsync();

      return xAddress.IdxAddress;

    }

    public async Task<int> Update(Address xAddress) {
      //funkcja aktualiuzująca adres
      //cAddress xAddress - adres do aktualizacji

      mDbContext.Update(xAddress);
      await mDbContext.SaveChangesAsync();
      return xAddress.IdxAddress;

    }

  }
}
