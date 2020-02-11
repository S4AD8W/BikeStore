using BikeStore.core.Domain.OrderNS;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Order;
using BikeStore.Infrastructure.Services.Order;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers.Order {
  public class CreateOrderHandler : ICommandHandler<CreateOrderCommand> {
    
    private readonly IOrderService mOrderService;

    public CreateOrderHandler(IOrderService xOrderService) {
      this.mOrderService = xOrderService;
    }

    public async Task<CommandResult> HandleAsync(CreateOrderCommand xCommand) {

      string pRedirecUrl = "";
      CommandResult pResult =  new CommandResult();
      int pIdxOrder = await mOrderService.CreateOrderAsync(xCommand.Ipv4, xCommand.Ipv6, xCommand.IdxUser, xCommand.GetDeliveryAddress(),
        xCommand.GetUserAddress(), xCommand.GetUserInvoiceAddress()
        );

      if(pIdxOrder == 0) {
        pResult.SetFailure(string.Empty);
        return pResult;
      }

      switch (xCommand.PaymentMethod) {
        case PaymentMethodEenum.CashOnDelivery:
        break;
        case PaymentMethodEenum.Transfer:
        break;
        case PaymentMethodEenum.PayU:
        pResult.SetSuccess("");
        pRedirecUrl = await mOrderService.PayOrderFromPayuAsync(pIdxOrder);
        pResult.RedirecURL = pRedirecUrl;
        break;
        default:
        break;
      }

      return pResult;
    }
  }
}
