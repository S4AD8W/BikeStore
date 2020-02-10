using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.Order {
public interface IOrderService :IService {

    Task<string> PayOrderFromPayuAsync(int xIdxOrder);
    Task<int> CreateOrderAsync(string xIpv4, string xIpv6, int xIdxUser, Address xDeliveryAddress, Address xInstallmentAddress, Address xInvoiceAdress);
    //Task<IEnumerable<cUserOrderInfoDTO>> GetUserOrderInfoAsync(int xIdxUser);
    //Task<cDetailOrderInformationDTO> GetDetailOrderAsync(int xIdxOrder);
  }
}
