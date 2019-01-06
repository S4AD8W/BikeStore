using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Repositories {
  interface IProductsRepository : IRepository {

    Task AddProduct(Product xProduct);
    Task DeleteProduct(Product xProduct);
    Task DeleteProduct(int xId);
    Task<IEnumerable<Product>> GetAllProduct();
  }
}
