using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands.Cart;
using BikeStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;

namespace BikeStore.Infrastructure.Services.Cart {
  public class CardtService : ICartService {

    private readonly SessionCart mSessionCart;
    private readonly IHttpContextAccessor mHttpContext;
    private readonly IProductsRepository mProductRepository;

    public CardtService(SessionCart xSessionCart, IHttpContextAccessor xHttpContent, IProductsRepository xProductsRepository) {
      mSessionCart = xSessionCart;
      mHttpContext = xHttpContent;
      mProductRepository = xProductsRepository;

    }

    public async Task<bool> AddProductAsync(AddProductCommand xCommand) {

      if (!await this.AddProductForUnknownUser(xCommand.IdxProduct, xCommand.Quantity)) return false;

      return true;

    }

    public Task<int> GetItemCartAsyc() {
      throw new NotImplementedException();
    }

    public Task<bool> RemoveProduct(RemoveProductCommand xCommand) {
      throw new NotImplementedException();
    }


    private async Task<bool> AddProductForUnknownUser(int xIdxProduct, int xQuantiti) {

      var pProduct = await mProductRepository.GetAsync(xIdxProduct);
      if (pProduct == null) return false;

      mSessionCart.AddItem(pProduct,xQuantiti);
      return true;
    }

    

  }
}
