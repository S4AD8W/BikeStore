using BikeStore.core.Domain;
using BikeStore.core.Domain.Product_NS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Repositories {
 public interface IProductsRepository : IRepository {

    Task<int> AddProductAsync(Product xProduct);
    Task DeleteProductAsync(Product xProduct);
    Task DeleteProductAsync(int xId);
    Task<IEnumerable<Product>> GetAllProductAsync();
    IQueryable<Product> Product { get; }
    Task EditProductAsync(Product xProduct);
  }
}
