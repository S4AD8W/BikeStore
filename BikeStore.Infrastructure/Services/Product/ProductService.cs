using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands.Product;
using BikeStore.core.Domain.Product_NS;

namespace BikeStore.Infrastructure.Services.Product_NS {
  public class ProductService : IProductService {

    private readonly IProductsRepository mProductRepository;

    public ProductService(IProductsRepository xProductRepository) {
      mProductRepository = xProductRepository;
    }

    public Task<bool> AddNewProduct(AddProductCommand xCommand) {

      Product pProduct = new Product();
      
      throw new NotImplementedException();
    }
  }
}
