using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Domain;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands.Cart;
using BikeStore.Infrastructure.Extensions;
using BikeStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;

namespace BikeStore.Infrastructure.Services.Carts {
  public class CartService : ICartService {
      
    private readonly IHttpContextAccessor mContext;
    private readonly IProductsRepository mProductRepository;

    public CartService(IHttpContextAccessor xHttpContent, IProductsRepository xProductsRepository) {
    //  mSessionCart = xSessionCart;
      mContext = xHttpContent;
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
      
      var pCart = mContext.HttpContext.Session.GetCart();
      if (pCart == null) pCart = new Cart();
      pCart.AddItem(pProduct, xQuantiti);

      mContext.HttpContext.Session.SetJson(SessionEnum.Cart.ToString(), pCart);

      return true;

    }

    

  }
}
