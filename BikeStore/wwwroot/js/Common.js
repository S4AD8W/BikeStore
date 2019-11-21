var jsKeyCode = {
  Enter: 13,
  Escape: 27,
};


var JsCommon = {

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
	SetCssClassForElement(xElement, xCssClass) {
		//funkcja dodająca klase css dla elementu html

		if (!xElement.classList.contains(xCssClass)) {
			xElement.classList.add(xCssClass);
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
