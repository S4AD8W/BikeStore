using BikeStore.core.Domain.Product_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Repositories {
 public interface IProductImageRepository : IRepository {

    IQueryable<ProductImage> ProductsImages { get; }
    Task<int> AddAsync(ProductImage xProductImage);
    Task UpdateAsync(ProductImage xProductImage);
    Task DeleteAsync(ProductImage xProductImage);
    Task DeleteAsync(int xIdxProducImage);
    Task<IEnumerable<ProductImage>> GetAllAsync();
    Task<ProductImage> GetProductImage(int xIdxProductImage);
    Task<IEnumerable<ProductImage>> GetAllImageForIdxProduct(int xIdxProduct);
  }
}
