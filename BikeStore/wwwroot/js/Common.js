var jsKeyCode = {
  Enter: 13,
  Escape: 27,
};


var JsCommon = {

  DXToastType: {
    Info: "info",
    Custom: "custom",
    Error: "error",
    Success: "success",
    warning: "warning"
  },

  GetPopUp: function (xPopUpId) {
    //funkcja pokazująca popupa 
    //xPopUpId - id okienka 

    document.getElementById(xPopUpId).classList.remove("hidden");

  },

  ClosePopUp: function (xPopUpId) {
    //funkcja zamykająca okienko popup
    //xPopUpId - id okienka 

    let pElement = document.getElementById(xPopUpId);

    if (!pElement.classList.contains("hidden")) {
      pElement.classList.add("hidden");
    }

  },

  ResetForm: function (xIdForm) {
    //funkcja restująca formularz
    //xIdForm - id formularza do zresetowania 

    document.getElementById(xIdForm).rset();

  },

  SetCookie: function (cname, cvalue, exdays) {

    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";

  },

  GetCookie: function (xName) {

    var name = xName + "=";

    var ca = document.cookie.split(';');

    for (var i = 0; i < ca.length; i++) {
      var c = ca[i];
      while (c.charAt(0) === ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) === 0) {
        return c.substring(name.length, c.length);
      }
    }

    return "";

  },

  DXShowToast: function (xText, xDisplayTime_Optional, xType_Optional) {
    //funkcja pokazująca toast 
    //xText - tekst który ma być wyświetlony
    //xDisplayTime_Optional - czas na jaki ma być pokazany toast
    //xType_Optional -  rodzaj stylu wyświetlanego toast

    let pDisplayTime = 1500,
      pTypeToast = JsCommon.DXToastType.Info;

    if (xDisplayTime_Optional !== undefined) { pDisplayTime = xDisplayTime_Optional; }
    if (xType_Optional !== undefined) { pTypeToast = xType_Optional; }

    $("#DXToastCommon").dxToast({
      message: xText,
      type: pTypeToast, //'custom' | 'error' | 'info' | 'success' | 'warning'
      displayTime: 1500
    });

    $("#DXToastCommon").dxToast("show");

  },

  CustomDropDawn(xEvent) {
    //funcka pokazująca kolory w stylu dropdawn list
    //xElement - aktualny element
    //xSetVisible_ID - identfikator elementu który ma zostać pokazanny

    let pDropDown = document.getElementById(xEvent.currentTarget.dataset.target); //pobranie elementu który ma być pokazany

    if (!pDropDown.classList.contains("dropdown-content-Active")) {// sprawdzenie czy przypadkiem wybrany element niejest już widoczny
      pDropDown.classList.add("dropdown-content-Active");   //dodanie dla elementu klase active
      //↓sprawdzenie czy strzałka ma ustawioną odpowiednią klase
      if (!xEvent.currentTarget.children[0].childNodes[1].classList.contains("ImgDropDownListActive")) { xEvent.currentTarget.children[0].childNodes[1].classList.add("ImgDropDownListActive"); }
    } else {
      pDropDown.classList.remove("dropdown-content-Active"); //usuniecie klasy aktive ukrycie elementu
      //↓ spradzenie czy strzłka jest ustwiona w odpowiednią strone
      if (xEvent.currentTarget.children[0].childNodes[1].classList.contains("ImgDropDownListActive")) { xEvent.currentTarget.children[0].childNodes[1].classList.remove("ImgDropDownListActive"); }
    }

  },

  AutoGrow(event) {
    //funkcja powudująca automatyczne rośniecie elementu 
    //event - zdarzenie globalne nasłuchiwacza 

    let pElementHeight = event.currentTarget.clientHeight;
    if (event.currentTarget.scrollHeight > pElementHeight) {
      event.currentTarget.style.height = (event.currentTarget.scrollHeight) + "px";
    }

  },

  HiddenElement(event, xElement) {
    //funkcja ukrywająca element
    //event - obiekt glbalnego zdarzenia nasłuchiwacza
    debugger;
    let pElement = undefined;

    if (xElement !== undefined) {
      pElement = xElement;
    } else {
      pElement = document.getElementById(event.currentTarget.dataset.target);
    }

    if (!pElement.classList.contains("hidden")) {
      pElement.classList.add("hidden");
    }

  },

  SetVisibleElement(event, xElement) {
    //funkcja ukrywająca element
    //event - obiekt glbalnego zdarzenia nasłuchiwacza

    let pElement = undefined;

    if (xElement !== undefined) {
      pElement = xElement;
    } else {
      pElement = document.getElementById(event.currentTarget.dataset.target);
    }
    if (pElement.classList.contains("hidden")) {
      pElement.classList.remove("hidden");
    }

  },

  ObjectArrayToArray(xFormArray) {
    //funkcja zmieniająca toablicę obiektów na jeden objekt
    //xFromArray tablica obiektów z formularza

    var pReturnArray = {};
    for (var i = 0; i < xFormArray.length; i++) {
      pReturnArray[xFormArray[i]['name']] = xFormArray[i]['value'];
    }
    return pReturnArray;
  },

  MarkElemntAsSelected(xIdentiti, xCssClass) {
    //funkcja zaznaczająca wybrany elemnt na stronie jako wybrany
    //xIdentiti - identyfikator elementu kóry jest wybarany
    //xCssClass - unikalna klasa css dla grupy elemntów z których jest jeden wybierany

    let pElements = document.getElementsByClassName(xCssClass);

    for (var i = 0; i < pElements.length; i++) {
      pElements[i].classList.remove("DisableElementDesigning");
      if (pElements[i].attributes.psidentiti.value === xIdentiti) continue;
      if (pElements[i].classList.contains("DisableElementDesigning")) continue;
      pElements[i].classList.add("DisableElementDesigning");
    }

  },

  RefreshLeftChoiceInfoMenu() {
    //funkcja odsiwierzająca pływające menu po lewej stronie

    let pMenuContainer = document.getElementById("FloatingLeftMenu");

    $.post("/Designing/RefreshLeftChoiceInfoMenu")
      .done(function (xData) {
        pMenuContainer.innerHTML = xData;
      })
      .fail(() => window.location.reload());

  },

  SetDisableAtrribute(xElement) {
    //funkcja ustawiająca dla wybranego elementu atrybut disabled
    //xElement - element dla którego ma być dodany atrybut 

    if (xElement.hasAttribute("disabled")) return;

    xElement.disabled = true;

  },

  GetElementPathFromEventLisener(xEvent) {
    //funcka zwracająca scieżkę do elemntu dla rużnych przeglądarek
    //xEvent - globalne zdarzenie addeventlisenera 

    let pPath = event.path || (event.composedPath && event.composedPath());

    return pPath;

  },

  CheckCurrentDesigningModule(xToMatchSubStrUri) {
    //funkcja sprawdzająca na podstawie Uri aktywny tryp projektowania 
    //xToMatchSubStrUri - string do prównania z Uri

    if (window.location.pathname.includes(xToMatchSubStrUri)) {  //sprawdzenie trybu projektowania na podstawie uri
      return true;
    }

    return false;

  },

  GoToElemenTopView(xId, xElement) {
    //funkcja scrolująca dokument do wskazanego elementu
    //xId - id elementu do pokazania
    //xElement - element do pokazania

    let pElement = undefined;

    if (xId !== undefined) {
      pElement = document.getElementById(xId);
    } else {
      pElement = xElement;
    }

    $('html, body').animate({
      scrollTop: $(pElement).offset().top
    }, 1000);

  },
 
  GoToProfileSystem() {
    //funkcja odpowiadająca za przejście do systemu profilowego

    let pProfileSystemRow = document.getElementById("ProfileSystem_Row");

    $.get("ProfileSystems")
      .done(xData => {
        pProfileSystemRow.innerHTML = xData;
        JsCommon.RefreshLeftChoiceInfoMenu();
        JsCommon.GoToElemenTopView("ProfileSystem_Row");
        JsDesigning.ProfileSystems.PrepareToView();
        $("#LPProjectPattern").dxLoadPanel("instance").hide();
      })
      .fail(xData => {

      });

  },

  GoToDimensions() {
    //funkcja przechądząca do karty wymiarowania

    $.get("Dimensions")
      .done(xData => {
        debugger;
        JsDesigning.Dimensions.PrepareToView(xData);
        JsCommon.RefreshLeftChoiceInfoMenu();
      })
      .fail(xData => {
        alert("fail from go to dimensions");
      });
  },

  GoToDetailsConfiguration() {
    //funkcja przechądząca do karty detali konfiguracji 

    $.get("Wings")
      .done(xData => {
        let pWingsContainer = document.getElementById("WingsConfiguration");
        pWingsContainer.innerHTML = xData;
        $("#WingsConfigurationTab").tab('show');
        JsDesigning.WingDesigning.PrepareToView();
        JsCommon.GoToElemenTopView("WingsConfiguration");
      })
      .fail(xData => {

      });

  },

  GoToSummary() {
    //funkcja przechodząca do karty podsumowania
    
    $.get("Summary")
      .done(xData => {
        document.getElementById("Summary").innerHTML=xData;
        JsDesigning.Summary.PrepareView();
        $("#WindowSummary").tab('show');
      })
      .fail(xData => {

      });
  },

  GoToOpeningSide() {
    //funkcja przechodząca do wyboru sposobu otwierania 
    
    $.get("OpeningSide")
      .done(xData => {
        document.getElementById("OpeningSideRow").innerHTML = xData;
        JsDesigning.OpeningSide.PrepareView();
        JsCommon.RefreshLeftChoiceInfoMenu();
      })
      .fail(xData => {

      });

  },

  DownloadViewPanels() {
    //funkcja pobierająca widok z panelami 

    $.get("Panel")
      .done(xData => {
        JsDesigning.Panels.PrepareToView(xData);
      })
      .fail(xData => {
        return false;
      });
  },

  DownloadViewAdditives() {
    //funkcja pobierająca widok z dodatkami

    $.get("Additives")
      .done(xData => {
        JsDesigning.Additives.PrepareToView(xData);
      })
      .fail(xData => {
        return false;
      });
  }

};

function toJSONString(form) {
  var obj = {};
  var elements = form.querySelectorAll("input, select, textarea");
  for (var i = 0; i < elements.length; ++i) {
    var element = elements[i];
    var name = element.name;
    var value = element.value;

    if (name) {
      obj[name] = value;
    }
  }
}
