using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.HttpClient {
  public interface IPayUHttpClient : IService {


    Task<cOAutchData> GetCredentials_OAuthAsync();
    Task<cPayUOrderResponse> SendNewOrderAsync(cOAutchData xAutchData, cPayUOrderInformation xOrderInformation);
    Task DeleteToken(cOAutchData xOAutchData);
  }
}
