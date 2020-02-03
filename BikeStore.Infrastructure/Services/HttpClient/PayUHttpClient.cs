using BikeStore.Infrastructure.Settings;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.HttpClient {
  public class PayUHttpClient : IPayUHttpClient{

    private readonly PayUSettings mPayUSettings;
    private readonly IHttpClientFactory mHttpClientFactory;

    public PayUHttpClient(PayUSettings xPayUSettings, IHttpClientFactory xClient) {
      mHttpClientFactory = xClient;
      mPayUSettings = xPayUSettings;
    }

    public async Task DeleteToken(cOAutchData xOAutchData) {
      //funkcja usówająca token autoryzacji
      //xOAutchData - dane tokena

      string pReguestAddres;

      pReguestAddres = mPayUSettings.ApiUri + "/tkoens";
      var pHttpClient = mHttpClientFactory.CreateClient();
      pHttpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", $"{xOAutchData.token_type} {xOAutchData.access_token}");
      var pContent = new StringContent($"token_id={xOAutchData.access_token}", System.Text.Encoding.Default, "application/x-www-form-urlencoded");
      var pResponse = await pHttpClient.PostAsync(pReguestAddres, pContent);
      //   pResponse.EnsureSuccessStatusCode();
      var pResponse_Byte = await pResponse.Content.ReadAsByteArrayAsync();

    }

    public async Task<cOAutchData> GetCredentials_OAuthAsync() {
      //funkcja zwracjąca dane autoryzacji płatność 

      cOAutchData pAutchData;

      var pHttpClient = mHttpClientFactory.CreateClient();

      var pContent = new StringContent($"grant_type=client_credentials&client_id={mPayUSettings.Client_Id}&client_secret={mPayUSettings.Client_Secret}", System.Text.Encoding.Default, "application/x-www-form-urlencoded");

      var pResponse = await pHttpClient.PostAsync(mPayUSettings.OAuth_Uri, pContent);
      pResponse.EnsureSuccessStatusCode();
      var pResponse_Byte = await pResponse.Content.ReadAsByteArrayAsync();
      pAutchData = Newtonsoft.Json.JsonConvert.DeserializeObject<cOAutchData>(System.Text.Encoding.UTF8.GetString(pResponse_Byte));

      return pAutchData;

    }


    public async Task<cPayUOrderResponse> SendNewOrderAsync(cOAutchData xAutchData, cPayUOrderInformation xOrderInformation) {
      //funkcja wysłająca nowe zamówienie do systemu płatność 
      //xAutchData - dane autoryzujące płatność
      //xOrderInformation -  dane o zamówieniu 

      StringContent pContent;
      string pResponseStr;
      string pAddress;
      string pRedirectUrl;
      HttpClientHandler pHttpHandler;
      cPayUOrderResponse pOrderResponse;

      pHttpHandler = new HttpClientHandler() {
        AllowAutoRedirect = false,

      };
      pAddress = mPayUSettings.ApiUri + "/orders";
      //[IW 12.03.2019] Siwiadomie użyłem nie dokońca poprawniej implemetacji httpclienta ze wzglendu że przez IhttpClientFactory 
      //niewiem jak wyłączyć domyślne ustawienie podązania za 302 statusem 
      using (pContent = new StringContent(await CreateStringContent(xOrderInformation), Encoding.Default, "application/json")) { //utworzenie kontentu
        using (var pHttpClient = new System.Net.Http.HttpClient(pHttpHandler)) { //utworzenie httpClienta
          pHttpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", $"{xAutchData.token_type} {xAutchData.access_token}");
          pHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          var pResponse = await pHttpClient.PostAsync(pAddress, pContent);
          pRedirectUrl = pResponse.StatusCode == System.Net.HttpStatusCode.Redirect ? pResponse.Headers.Location.OriginalString : null;
          var pArrayResponsed = await pResponse.Content.ReadAsByteArrayAsync();
          pResponseStr = Encoding.UTF8.GetString(pArrayResponsed);
          pOrderResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<cPayUOrderResponse>(pResponseStr);
        }
      }

      return pOrderResponse;

    }

    private async Task<string> CreateStringContent(cPayUOrderInformation xOrderInformation) {
      //funkcja wrapująca jesona z danumi zamówienia
      //xOrderDTO - dane zmówienia

      StringBuilder pStringBuilder;
      pStringBuilder = new StringBuilder();

      pStringBuilder.Append("{");
      pStringBuilder.Append($"\"notifyUrl\": \"{xOrderInformation.NotifiUrl}\", ");
      pStringBuilder.Append($"\"customerIp\": \"{xOrderInformation.UserIp}\", ");
      pStringBuilder.Append($"\"merchantPosId\": \"{mPayUSettings.Client_Id}\", ");
      pStringBuilder.Append($"\"description\": \"Test\", ");      //Opis płatność/uwagi płatność 
      pStringBuilder.Append($"\"currencyCode\": \"{xOrderInformation.CurencCode}\", "); //Kod waluty wymagane
      pStringBuilder.Append($"\"totalAmount\": \"{xOrderInformation.Price}\", ");         //wymagna postać najniszego nominału waluty PLN to 1gr
      //[IW 12.03.2019]Sekcja danych kupującego jest bardzo ważna narazie zakomentowłem żeby testować api PayU
      //pStringBuilder.Append("\"buyer\": { ");
      //pStringBuilder.Append($"\"email\": \"{xOrderInformation.BuyerEmail}\", ");
      //pStringBuilder.Append($"\"phone\": \"{xOrderInformation.BuyerPhone}\", ");
      //pStringBuilder.Append($"\"firstName\": \"{xOrderInformation.BuyerFirstName}\", ");
      //pStringBuilder.Append($"\"lastName\": \"{xOrderInformation.BuyerLastName}\", ");
      //pStringBuilder.Append($"\"language\": \"{xOrderInformation.BuyerLanguage}\" ");
      //pStringBuilder.Append(" },");
      pStringBuilder.Append("\"products\": [ ");

      foreach (var pItem in xOrderInformation.OrderItems) {
        pStringBuilder.Append("{ ");
        pStringBuilder.Append($"\"name\": \"{pItem.Name}\", ");
        pStringBuilder.Append($"\"unitPrice\": \"/555\", ");
        pStringBuilder.Append($"\"quantity\": \"{pItem.Quantiti}\"");
        pStringBuilder.Append(" },");
      }
      pStringBuilder.Length -= 1;                                      //Usuniecie przecinka po ostatnim prodkcie na liscie 
      pStringBuilder.Append("]}");

      await Task.CompletedTask;

      return pStringBuilder.ToString();

    }
  }
}