using BikeStore.core.Domain.Product_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Repositories {

  public interface IProductsCategoryRepository : IRepository {

    IQueryable<ProductCategory> ProductsCategory { get; }
    Task<int> AddAsync(ProductCategory xProductCategory);
    Task<ProductCategory> GetAsync(int xIdxCategory);
    Task UpdateAsync(ProductCategory xProducCateogry);
    Task DeleteAsync(int xIdxProductCategory);
    Task DeleteAsync(ProductCategory xProductCategory);
  }
}
