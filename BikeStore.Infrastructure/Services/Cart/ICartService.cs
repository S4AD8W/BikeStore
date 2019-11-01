using BikeStore.Infrastructure.Commands.Cart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.Carts {
  public interface ICartService : IService {

    Task<bool> AddProductAsync(AddProductCommand xCommand);
    Task<bool> RemoveProduct(RemoveProductCommand xCommand);
    Task<int> GetItemCartAsyc();

  }
}
