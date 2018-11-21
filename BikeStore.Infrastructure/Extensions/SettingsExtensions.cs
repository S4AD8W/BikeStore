using Microsoft.Extensions.Configuration;


namespace BikeStore.Infrastructure.Extensions {

  public static class SettingsExtensions {
    //Klasa pobierająca ustawienia aplikacji z pliku appSetings.json
    public static T GetSettings<T>(this IConfiguration xConfiguration) where T : new() {

      var pSection = typeof(T).Name.Replace("Settings", string.Empty);
      var pConfigurationValue = new T();
      xConfiguration.GetSection(pSection).Bind(pConfigurationValue);

      return pConfigurationValue;                           //zwrócenie obiektu przenoszącego konfigurację 

    }

  }
}