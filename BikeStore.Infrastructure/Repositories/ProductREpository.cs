﻿using BikeStore.core.Domain;
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
  public class ProductsRepository : IProductsRepository {

    private readonly BikeStoreContext mDB;
    public ProductsRepository(BikeStoreContext xDB) {
      mDB = xDB;
    }

    public IQueryable<Product> Products => mDB.Product;


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

      Product pProduct = await mDB.Product.SingleOrDefaultAsync(x => x.IdxProduct == xId);

      if (pProduct != null) {
        mDB.Remove(pProduct);
        await mDB.SaveChangesAsync();
      }

    }

    public async Task EditProductAsync(Product xProduct) {

     // mDB.Update(xProduct);
      //mDB.SaveChanges();
      using (BikeStoreContext xDB = new BikeStoreContext()) {
        xDB.Update(xProduct);
        xDB.SaveChanges();
      }
    }

    public async Task<IEnumerable<Product>> GetAllProductAsync()
      => await mDB.Product.ToListAsync();

    public async Task<Product> GetAsync(int xidxProduct)
    => await mDB.Product.FirstOrDefaultAsync(x => x.IdxProduct == xidxProduct);

  }
}
