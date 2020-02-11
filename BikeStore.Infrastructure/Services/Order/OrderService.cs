using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Domain.OrderNS;
using BikeStore.Infrastructure.Services.Carts;
using BikeStore.core.Repositories;
using BikeStore.core.Domain.OrderNS.Repository;
using BikeStore.Infrastructure.Types;
using Microsoft.AspNetCore.Http;
using BikeStore.Infrastructure.Services.HttpClient;

namespace BikeStore.Infrastructure.Services.Order {

  public class OrderService : IOrderService {

    private readonly ICartService mCartService;
    private readonly IAddressReposiory mAddressesRepository;
    private readonly IOrderRepository mOrderRepository;
    private readonly IOrderItemsRepository mOrdersItemsRepository;
    private readonly IUserRepository mUserRepository;
    private readonly IHttpContextAccessor mHttpContext;
    private readonly IPayUHttpClient mPayUClient;

    public OrderService(ICartService xCartService, IAddressReposiory xAddressesRepository, IOrderRepository xOrderRepository,
      IOrderItemsRepository xOrdersItemsRepository, IUserRepository xUserRepository, IHttpContextAccessor xHttpContext, IPayUHttpClient xPayUClient) {
      mCartService = xCartService;
      mAddressesRepository = xAddressesRepository;
      mOrderRepository = xOrderRepository;
      mOrdersItemsRepository = xOrdersItemsRepository;
      mUserRepository = xUserRepository;
      mHttpContext = xHttpContext;
      mPayUClient = xPayUClient;
    }
    public async Task<int> CreateOrderAsync(string xIpv4, string xIpv6, int xIdxUser, Address xDeliveryAddress, Address xInstallmentAddress, Address xInvoiceAdress) {
      Cart pCart;
      BikeStore.core.Domain.OrderNS.Order  pOrder;
      List<OrderItem> pOrderItems;
      OrderItem pOrderItem;
      int pIdxOrder;

      var pAddresses = await this.SaveOrderAddresses(xDeliveryAddress, xInstallmentAddress, xInvoiceAdress);
      pOrderItems = new List<OrderItem>();
      pCart = await mCartService.GetCartAsync(xIdxUser);
      pOrder = new core.Domain.OrderNS.Order(xIpv4, xIpv6, xIdxUser,pAddresses.xIdxDeliveryAddress.Value, pAddresses.xIdxInstallmentAddres.Value);
      

      pIdxOrder = await mOrderRepository.AddOrderAsync(pOrder);

      foreach (var pItem in pCart.Lines) {
        pOrderItem = new OrderItem(pOrder.IdxOrder,pItem.Product.Name, pItem.Quantity, pItem.Product.Price, pItem.Product.IdxProduct);
        pOrderItems.Add(pOrderItem);
      }

      await mOrdersItemsRepository.AddOrderItemsAsync(pOrderItems);

      //await mCartService.DeleteCart(pCart.IdxCart);

      return pIdxOrder;
    }

    public async Task<string> PayOrderFromPayuAsync(int xIdxOrder) {
      cPayUOrderInformation pOrderInformation;
      cOAutchData pOAutchData;
      string pRdirectURI;
      cPayUOrderResponse pPayuResponse;
      core.Domain.OrderNS.Order pOrder;
      User pUser;


      pOrder = await mOrderRepository.GetOrder(xIdxOrder);
      pUser = await mUserRepository.GetAsync(pOrder.IdxUser);

      pOrderInformation = new cPayUOrderInformation {
        BuyerEmail = pUser.Email,
        BuyerFirstName = pUser.Name,
        BuyerLastName = pUser.Surname,
        BuyerPhone = "",
        Price = 555,  //await mOrderRepository.GetPriceOrder(xIdxOrder),
        UserIp = mHttpContext.HttpContext.Connection.RemoteIpAddress.ToString(),
        OrderItems = await mOrdersItemsRepository.GetOrderItems(xIdxOrder),
        NotifiUrl = $@"Http://{mHttpContext.HttpContext.Request.Host.Host}/Account/ConfirmEmail"

      };

      pRdirectURI = string.Empty;
      pOAutchData = await mPayUClient.GetCredentials_OAuthAsync(); //zalogowanie się do systemu płatność

      pPayuResponse = await mPayUClient.SendNewOrderAsync(pOAutchData, pOrderInformation);//utworzenie płatność 
      if (!string.IsNullOrWhiteSpace(pPayuResponse.RedirectUri)) {
        pRdirectURI = pPayuResponse.RedirectUri;
        await mPayUClient.DeleteToken(pOAutchData);
      }

      return pRdirectURI;

    }


    private async Task<(int? xIdxInstallmentAddres, int? xIdxInvoiceAddress, int? xIdxDeliveryAddress)> SaveOrderAddresses(
   Address xDeliveryAddress, Address xInstallmentAddress, Address xInvoiceAddress) {
      //funkcja zwracająca indeksy zapisanych adresów 
      //xIdxDeleliveryAddress - adres dostawy
      //xIdxInstallmentAddress - adres montarzu 
      //xIdxInvoiceAddress - adres do faktury

      int? pIdxInvoiceAddress;
      int? pIdxDeliveryAddress;
      int? pIdxInstallmentAddress;

      pIdxInvoiceAddress = null;
      pIdxDeliveryAddress = null;
      pIdxInstallmentAddress = null;

      if (xInstallmentAddress != null) pIdxInstallmentAddress = await mAddressesRepository.AddAsync(xInstallmentAddress);
      if (xDeliveryAddress != null) pIdxDeliveryAddress = await mAddressesRepository.AddAsync(xDeliveryAddress);
      if (xInvoiceAddress != null) pIdxInvoiceAddress = await mAddressesRepository.AddAsync(xInvoiceAddress);

      return (xIdxInstallmentAddres: pIdxInstallmentAddress, xIdxInvoiceAddress: pIdxInvoiceAddress, xIdxDeliveryAddress: pIdxDeliveryAddress);

    }

  }

}
