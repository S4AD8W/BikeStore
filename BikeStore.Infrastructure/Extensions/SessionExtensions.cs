using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BikeStore.Infrastructure.Extensions {

  public enum SessionEnum {

    WindowProject = 1,

  }

  public static class SessionExtensions {
    //Klasa rozszerzając interfejs sesji http
    
    public static void SetJson(this ISession xSession, string xKey,
        object value) => xSession.SetString(xKey, JsonConvert.SerializeObject(value)); //zapisanie jsona do zmiennej sesji 


    public static T GetJson<T>(this ISession xSession, string xKey) {
      //funkcja generyczna  pobierająca jsona z sesji i rzutująca na typ 

      var pSessionData = xSession.GetString(xKey);          ////pobranie  danych z sesji  na podstawie klucza 

      return pSessionData == null  ? default(T) : JsonConvert.DeserializeObject<T>(pSessionData); //zwrócenie obiektu 

    }

  }
}