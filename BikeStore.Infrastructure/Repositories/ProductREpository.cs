using BikeStore.core.Domain;
using BikeStore.core.Domain.Product_NS;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Repositories {
  public class ProductsRpository : IProductsRepository {

    private readonly BikeStoreContext mDB;
    public ProductsRpository(BikeStoreContext xDB) {
      mDB = xDB;
    }

    public IQueryable<Product> Product => mDB.Product;

    public async Task<int> AddProductAsync(Product xProduct) {
      await mDB.Product.AddAsync(xProduct);
      await mDB.SaveChangesAsync();
      return xProduct.IdxProduct;
    }

    public async Task DeleteProductAsync(Product xProduct) {
      mDB.Remove(xProduct);
      await mDB.SaveChangesAsync();

    }

    public async Task DeleteProductAsync(int xId) {

      Product pProduct = await mDB.Product.SingleOrDefaultAsync(x => x.IdxProduct ==xId);

      if (pProduct != null) {
        mDB.Remove(pProduct);
        await mDB.SaveChangesAsync();
      }

    }

    public async Task EditProductAsync(Product xProduct) {
      mDB.Entry(xProduct).State = EntityState.Modified; // edycja systemu profilowego w kontekscie bazy danych 
      await mDB.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductAsync()
      => await mDB.Product.ToListAsync();
  }
}
