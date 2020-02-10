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

    public async Task<Cart> GetCartAsync(int xIdxUser) {
      Cart pCart;
      Guid pUserId;

      if (mContext.HttpContext.User.Identity.IsAuthenticated) {//sprawdzenie czy użytkownik jest zalogowany 
        Guid.TryParse(mContext.HttpContext.User.Identity.Name, out pUserId);//pruba utorzenia id użytkownika
       //pCart = await this.GetCartForLogInUserAsync(pUserId);// wywołanie funkcji pobierającej karte dla zalogowanego użytkownika
        pCart = mContext.HttpContext.Session.GetCart(); // pobranie karty z zmienej sesji 
      } else {
        pCart = mContext.HttpContext.Session.GetCart(); // pobranie karty z zmienej sesji 
      }

      return pCart;

    }
  

    public Task<int> GetItemCartAsyc() {
      throw new NotImplementedException();
    }

    public async Task<bool> RemoveProduct(RemoveProductCommand xCommand) {
      Cart pCart;
      Guid pUserId;

      if (mContext.HttpContext.User.Identity.IsAuthenticated) {//sprawdzenie czy użytkownik jest zalogowany 
        Guid.TryParse(mContext.HttpContext.User.Identity.Name, out pUserId);//pruba utorzenia id użytkownika
        await mProductRepository.DeleteProductAsync(xCommand.IdxProduct);
        if (await this.GetCartItemsCountAsync() == 0) {
        //  await mCartsRepository.DeleteCartAsync(pUserId);
        }
      } else {
        pCart = mContext.HttpContext.Session.GetCart(); //pobranie karty z zmienej sesji 
       // await pCart.RemoveProduct_UseOnlyUnknownUsers(xProductId);//usuniecie prodkutu z karty 
       // mHttpContext.HttpContext.Session.SetJson(Convert.ToString(SessionEnum.UserCart), pCart);// zapisanie karty w zmienej sesji 
      }

      return true;
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

    public async Task<int> GetCartItemsCountAsync() {
      //funkcja zwracjąca informacje ile jest produktów w koszyku

      int pProductCount;
      Guid pUserId;
      Cart pCart;
      int? pIdxCart;

      //if (mContext.HttpContext.User.Identity.IsAuthenticated) {
      //  Guid.TryParse(mContext.HttpContext.User.Identity.Name, out pUserId);
      //  pIdxCart = await mCartsRepository.GetCartIdxAsync(pUserId);
      //  if (pIdxCart != null) {
      //    //pProductCount = mProductsRepository.Products.Count(x => x.IdxCart == pIdxCart);
      //  } else {
      //    pProductCount = 0;
      //  }
      //} else {
      //  pCart = mHttpContext.HttpContext.Session.GetCart();
      //  if (pCart != null) {
      //    pProductCount = pCart.Products.Count();
      //  } else {
      //    pProductCount = 0;
      //  }
      //}

      await Task.CompletedTask;

      return 2;

    }

  }
}
