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
    private readonly IProductImageRepository mProductImageRepository;

    public ProductService(IProductsRepository xProductRepository, IProductImageRepository xProductImageRepository) {
      mProductRepository = xProductRepository;
      mProductImageRepository = xProductImageRepository;
    }

    public async Task<bool> AddNewProductAsync(AddProductCommand xCommand) {

      Product pProduct = new Product(xCommand.Name, xCommand.Description, xCommand.Price, xCommand.IdxCategory);

      if (await mProductRepository.AddProductAsync(pProduct) ==0) return false;

      foreach (var pItem in xCommand.Images) {
        pItem.IdxProduct = pProduct.IdxProduct;
        await mProductImageRepository.AddAsync(pItem);
      }

      return true;

    }

  }
}
