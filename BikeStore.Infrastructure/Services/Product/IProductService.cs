using BikeStore.Infrastructure.Commands.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.Product_NS {
  public interface IProductService : IService {

    Task<bool> AddNewProductAsync(AddProductCommand xCommand);
    Task<bool> EditProductAsync(EditProductCommand xCommand);

  }
}
