using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static BikeStore.Infrastructure.Languages;

namespace BikeStore.Extensions {
  public class CustomRequiredAttribute : RequiredAttribute {
    private string propertyName;

    public TextEnum CntTextTranslate { get; set; }

    public CustomRequiredAttribute([CallerMemberName] string xPropertyName = null) {

      this.propertyName = xPropertyName;

    }


    public string PropertyName {
      //Funkcja zwracająca nazwe pola

      get { return this.propertyName; }

    }


    public override string FormatErrorMessage(string xName) {

      return string.Format(this.GetErrorMessage(), xName);

    }


    private string GetErrorMessage() {
      //funkcja pobierająca treść błedu

      return Translate(this.ErrorMessage);

    }

    private string Translate(string xMessaage) {
      //funkcja tłumacząca bład dla pola
      //xMessage - string z treścią błędu

      return cConfigurationLanguages.GetText(CntTextTranslate);

    }
  }
}
