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
  public class ProductCategory : IProductsCategoryRepository {

    private readonly BikeStoreContext mDB;

    public ProductCategory(BikeStoreContext xDB) {
      mDB = xDB;
    }

    public IQueryable<core.Domain.Product_NS.ProductCategory> ProductsCategory => mDB.ProductCategory;

    public async Task<int> AddAsync(core.Domain.Product_NS.ProductCategory xProductCategory) {
      mDB.ProductCategory.Add(xProductCategory);
      await mDB.SaveChangesAsync();
      return xProductCategory.IdxProductCategory;
    }

    public async Task DeleteAsync(int xIdxProductCategory) {
      var pProductCategory = await ProductsCategory.FirstOrDefaultAsync(x => x.IdxProductCategory == xIdxProductCategory);
      mDB.Remove(pProductCategory);
      await mDB.SaveChangesAsync();
    }

    public async Task DeleteAsync(core.Domain.Product_NS.ProductCategory xProductCategory) {
      mDB.Remove(xProductCategory);
      await mDB.SaveChangesAsync();
    }

    public async Task<core.Domain.Product_NS.ProductCategory> GetAsync(int xIdxCategory) {
      return await ProductsCategory.FirstOrDefaultAsync(x => x.IdxProductCategory == xIdxCategory);
    }

    public Task UpdateAsync(core.Domain.Product_NS.ProductCategory xProducCateogry) {
      throw new NotImplementedException();
    }
  }
}
