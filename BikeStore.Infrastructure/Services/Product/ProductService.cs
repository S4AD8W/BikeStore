using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands.Product;

namespace BikeStore.Infrastructure.Services.Product {
  public class ProductService : IProductService {

    private readonly IProductsRepository mProductRepository;

    public ProductService(IProductsRepository xProductRepository) {
      mProductRepository = xProductRepository;
    }

    public Task<bool> AddNewProduct(AddProductCommand xCommand) {
      throw new NotImplementedException();
    }
  }
}
