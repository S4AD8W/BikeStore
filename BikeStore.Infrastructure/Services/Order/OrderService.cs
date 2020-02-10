using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Domain.OrderNS;
using BikeStore.Infrastructure.Services.Carts;
using BikeStore.core.Repositories;
using BikeStore.core.Domain.OrderNS.Repository;

namespace BikeStore.Infrastructure.Services.Order {

  public class OrderService : IOrderService {

    private readonly ICartService mCartService;
    private readonly IAddressReposiory mAddressesRepository;
    private readonly IOrderRepository mOrderRepository;
    private readonly IOrderItemsRepository mOrdersItemsRepository;
    public OrderService(ICartService xCartService, IAddressReposiory xAddressesRepository, IOrderRepository xOrderRepository,
      IOrderItemsRepository xOrdersItemsRepository) {
      mCartService = xCartService;
      mAddressesRepository = xAddressesRepository;
      mOrderRepository = xOrderRepository;
      mOrdersItemsRepository = xOrdersItemsRepository;
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
      pOrder = new core.Domain.OrderNS.Order(xIpv4, xIpv6, xIdxUser,pAddresses.xIdxDeliveryAddress.Value, pAddresses.xIdxInvoiceAddress.Value);
      

      pIdxOrder = await mOrderRepository.AddOrderAsync(pOrder);

      foreach (var pItem in pCart.Lines) {
        pOrderItem = new OrderItem(pOrder.IdxOrder,pItem.Product.Name, pItem.Quantity, pItem.Product.Price, pItem.Product.IdxProduct);
        pOrderItems.Add(pOrderItem);
      }

      await mOrdersItemsRepository.AddOrderItemsAsync(pOrderItems);

      //await mCartService.DeleteCart(pCart.IdxCart);

      return pIdxOrder;
    }

    public Task<string> PayOrderFromPayuAsync(int xIdxOrder) {
      throw new NotImplementedException();
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
