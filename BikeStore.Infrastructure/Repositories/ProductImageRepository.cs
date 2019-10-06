using BikeStore.core.Domain.Product_NS;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Repositories {
  public class ProductImageRepository : IProductImageRepository {

    private readonly BikeStoreContext mDB;
   
    public ProductImageRepository(BikeStoreContext xDB) {
      mDB = xDB;
    }

    public async Task<int> AddAsync(ProductImage xProductImage) {
      await mDB.ProductImages.AddAsync(xProductImage);
      await mDB.SaveChangesAsync();

      return xProductImage.IdxProduct;

    }

    public async Task DeleteAsync(ProductImage xProductImage) {
      mDB.ProductImages.Remove(xProductImage);
      await mDB.SaveChangesAsync();
    }

    public async Task DeleteAsync(int xIdxProducImage) {
      var pProductImage = await mDB.ProductImages.FirstOrDefaultAsync(x => x.IdxProductImage == xIdxProducImage);
      mDB.ProductImages.Remove(pProductImage);
      await mDB.SaveChangesAsync();

    }

    public Task<IEnumerable<ProductImage>> GetAllAsync() {
      throw new NotImplementedException();
    }

    public Task<ProductImage> GetProductImage(int xIdxProductImage) {
      throw new NotImplementedException();
    }

    public Task UpdateAsync(ProductImage xProductImage) {
      throw new NotImplementedException();
    }
  }
}
