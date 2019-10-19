using BikeStore.core.Domain.Product_NS;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace BikeStore.Infrastructure.Repositories {
  public class ProductImageRepository : IProductImageRepository {

    private readonly BikeStoreContext mDB;
   
    public ProductImageRepository(BikeStoreContext xDB) {
      mDB = xDB;
    }

    public IQueryable<ProductImage> ProductsImages => mDB.ProductImages;

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

    public async Task<IEnumerable<ProductImage>> GetAllAsync() {
      return await mDB.ProductImages.ToListAsync();
    }

    public async Task<IEnumerable<ProductImage>> GetAllImageForIdxProduct(int xIdxProduct) {
      return await mDB.ProductImages.Where(x => x.IdxProduct == xIdxProduct).ToListAsync();
    }

    public async Task<ProductImage> GetProductImage(int xIdxProductImage) {
      return await mDB.ProductImages.FirstOrDefaultAsync(x => x.IdxProductImage == xIdxProductImage);
    }

    public async Task UpdateAsync(ProductImage xProductImage) {

      using (BikeStoreContext pDB = new BikeStoreContext()) {
        pDB.ProductImages.Update(xProductImage);
       await pDB.SaveChangesAsync();
      }

    }
  }
}
