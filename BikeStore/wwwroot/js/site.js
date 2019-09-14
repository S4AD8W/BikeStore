// Write your JavaScript code.



var JsCart = {

  ErrorMesChangeQuantiti: "",
  SuccesChangedQuantiti: "",
  WrongQuantiti: "",
  ConfirmChange: "",
  Save: "",
  SomthingWenWrong: "",
  TheNameOfPRoductIsCHanged: "",

  AddComent(event) {
    //funkcja pozwalająca dodać komentarz
    //event - globalne zdarzenie nasłuchiwacza

    let pCommentContainer = document.getElementById("CartComment"),
      pTextArea = pCommentContainer.getElementsByTagName("textarea")[0],
      pBntSaveComemnt = document.getElementById("SaveComment");

   
    event.preventDefault();

    if (pCommentContainer.classList.contains("hidden")) {
      pCommentContainer.classList.remove("hidden");
    }

    pBntSaveComemnt.classList.remove("hidden");
    pTextArea.removeAttribute("disabled");
    pTextArea.focus();
    pTextArea.select();

  },

  SaveComment(event) {
    //funkcja zapisująca komentarz do koszyka
    //event - globalny event zdarzenia nasłuchiwacza 

    let pIdxCart = document.getElementById("Cart").dataset.cartid,
      pCommentContainer = document.getElementById("CartComment"),
      pCartComment = pCommentContainer.getElementsByTagName("textarea")[0].value;

    event.preventDefault();

    $.post("cart/AddCartComment", {
      xComment: pCartComment,
    }).done(function () {
      window.location.reload();

    });

  },

  ChangeQuantityProduct (event) {
    //funkcja zmieniająca ilość produktu w koszyku
    //xValue - ilość produktu
    //xPorductIdx - inkeks produktu

    let pProductUuid = event.currentTarget.dataset.productid,
      pQuantity = parseInt(event.currentTarget.value),
      pLastQuantiti = parseInt(event.currentTarget.dataset.lastvalue),
      pInput = event.currentTarget;
      
    
    if (pQuantity >= 1) {
      $.post("/Cart/ChangeQuantityProduct", { xQuantity: pQuantity, xProductId: pProductUuid })
        .done(function (xData) {
          JsCommon.DXShowToast(JsCart.SuccesChangedQuantiti, undefined, JsCommon.DXToastType.Success);
        })
        .fail(function (xData) {
          pInput.value = pLastQuantiti;
          JsCommon.DXShowToast(JsCart.ErrorMesChangeQuantiti, undefined, JsCommon.DXToastType.Error);
        });
    } else {
     
      pInput.value = pLastQuantiti;
      JsCommon.DXShowToast(JsCart.WrongQuantiti, undefined, JsCommon.DXToastType.Info);
    }
  },

  EnableEditProductName: function (event) {
    //funkcja zmieniająca nazwę produktu
    //xElement - aktualny element
    //xProducId - id produktu
    //event - zdarzenie globalne dokumentu

    //inicjalizacja zmiennych
    let pSpanId = "ProductNameSN:" + event.currentTarget.dataset.productid,
      pInputId = "ProductNameIT:" + event.currentTarget.dataset.productid,
      pIsEdit = event.currentTarget.dataset.isedit,
      pSpan = document.getElementById(pSpanId),
      pInput = document.getElementById(pInputId),
      pLastName = event.currentTarget.dataset.lastname;;

    event.returnValue = false;                            //anulowanie zdażenia globalnego

    if (pIsEdit === "true") {                             //sprawdzenie czy edycja
      pSpan.classList.add("hidden");
      pInput.classList.remove("hidden");
      event.currentTarget.innerText = JsCart.ConfirmChange;
      event.currentTarget.dataset.isedit = "false";
      jsCart.mProjectNameAfterHange = pInput.value;
    } else {
      pSpan.classList.remove("hidden");
      pInput.classList.add("hidden");
      event.currentTarget.innerText = JsCart.Save;  //11 = edytuj
      event.currentTarget.dataset.isedit = "true";
      if (jsCart.mProjectNameAfterHange !== pInput.value) {
        jsCart.mProjectNameAfterHange = undefined;        //wysłanie nowej nazwy na serwer
        pSpan.innerHTML = pInput.value;
        
        $.post('/Cart/ChangeProductName', {
          xProductName: pInput.value,
          xProductId: event.currentTarget.dataset.productid
        })
          .done(function (xData) {
            JsCommon.DXShowToast(JsCart.TheNameOfPRoductIsCHanged, undefined, JsCommon.DXToastType.Success);
          })
          .fail(function (xData) {
            pSpan.innerHTML = pLastName;
            JsCommon.DXShowToast(JsCart.SomthingWenWrong, undefined, JsCommon.DXToastType.Error);
           
          });


      }

    }

  },

  KeyPresOnPaymentWizard(event) {
    //funkcja obsługująca klikniecie klawiatur w wizardzie płatność
    //event globalne zdarzenie nasłuchiwacza 

    debugger;
  },

  SetDataForInvoice(xEvent) {
    //funkcja pokazująca formulaż do uzupęłnienia danych dla faktury
    //xEvent - globalny event nasłuchiwacza
    
    let pIsWantGetInfoice = xEvent.currentTarget.checked,
      pDateForInvoiceContainer = document.getElementById("DataForInvoice");

    if (pIsWantGetInfoice) {
      JsCommon.SetVisibleElement(null, pDateForInvoiceContainer);
    } else {
      JsCommon.HiddenElement(null, pDateForInvoiceContainer);
    }


  },

  PaymentWizzard: {

    SetDeliveryAddressIfNotSet() {
      //funkcja ustawiajająca wartość pul z adresem w sekcji dostawy
      //CF => Confirm 

      
      let pForm = document.getElementById("PaymetWizardForm"),//Przygotowanie elementów 
        pCFFirstName = document.getElementById("CFFirstName"),
        pCFLastName = document.getElementById("CFLastName"),
        pCFCity = document.getElementById("CFCity"),
        pCFZipCode = document.getElementById("CFZipCode"),
        pCFStreet = document.getElementById("CFStreet"),
        pCFFlatNumber = document.getElementById("CFFlatNumber"),
        pCFPhone = document.getElementById("CFPhone"),
        pCFCountry = document.getElementById("CFCountry");
      

      pCFFirstName.innerText = pForm["FirstName"].value;    //Ustawienie wartość dla elemntów 
      pCFLastName.innerText = pForm["LastName"].value;
      pCFCity.innerText = pForm["City"].value;
      pCFZipCode.innerText = pForm["ZipCode"].value;
      pCFStreet.innerText = pForm["Street"].value;
      pCFFlatNumber.innerText = pForm["HouseNumber"].value;
      pCFPhone.innerText = pForm["Phone"].value;
      pCFCountry.innerText = pForm["Country"].value;
    },

    SetDateSummaryPayment() {
      //funkcja ustawiająca dane dla podsumowania płatność 
      //SM => summary
      //AD => AnotherDelivery

      let pForm = document.getElementById("PaymetWizardForm"),//Przygotowanie elementów 
        pSMFirstName = document.getElementById("SMFirstName"),
        pSMLastName = document.getElementById("SMLastName"),
        pSMCity = document.getElementById("SMCity"),
        pSMZipCode = document.getElementById("SMZipCode"),
        pSMStreet = document.getElementById("SMStreet"),
        pSMFlatNumber = document.getElementById("SMFlatNumber"),
        pSMPhone = document.getElementById("SMPhone"),
        pSMCountry = document.getElementById("SMCountry");
        pSMTrasferMethod = undefined;
        pSMTrasferMethod2 = undefined;


      if (pForm["AccountDeliveryAddress"].checked === true) {
        pSMFirstName.innerText = pForm["FirstName"].value;    //Ustawienie wartość dla elementów 
        pSMLastName.innerText = pForm["LastName"].value;
        pSMCity.innerText = pForm["City"].value;
        pSMZipCode.innerText = pForm["ZipCode"].value;
        pSMStreet.innerText = pForm["Street"].value;
        pSMFlatNumber.innerText = pForm["HouseNumber"].value;
        pSMPhone.innerText = pForm["Phone"].value;
        pSMCountry.innerText = pForm["Country"].value;
      } else {
        pSMFirstName.innerText = pForm["ADFirstName"].value;    //Ustawienie wartość dla elementów 
        pSMLastName.innerText = pForm["ADLastName"].value;
        pSMCity.innerText = pForm["ADCity"].value;
        pSMZipCode.innerText = pForm["ADZipCode"].value;
        pSMStreet.innerText = pForm["ADStreet"].value;
        pSMFlatNumber.innerText = pForm["ADHouseNumber"].value;
        pSMPhone.innerText = pForm["Phone"].value;
        pSMCountry.innerText = pForm["ADCountry"].value;
      }
      
      if (pForm["payPayU"].checked === true) {
        pSMTrasferMethod = document.getElementById("SMPayU");
        pSMTrasferMethod2 = document.getElementById("SMTransfer");
        JsCommon.HiddenElement(undefined, pSMTrasferMethod2);
        JsCommon.SetVisibleElement(undefined, pSMTrasferMethod);
      } else {
        pSMTrasferMethod = document.getElementById("SMPayU");
        pSMTrasferMethod2 = document.getElementById("SMTransfer");
        JsCommon.HiddenElement(undefined, pSMTrasferMethod);
        JsCommon.SetVisibleElement(undefined, pSMTrasferMethod2);
      }

    },

    SendPaymentFormFromWizar() {
      //Funkcja wysłająca formularz z danymi pochodzącymi z wizardu płatność 

      let pForm = document.getElementById("PaymetWizardForm");

      $("#PaymetWizardForm").submit(function (event) {

        event.preventDefault();
        var pForm = $(this);

        $.post("/order/PayForOrder", { xVM: pForm.serialize() })
          .done(function (xData) {
            alert("succes");
          })
          .fail(function (xData) {
            alert("test");
          });
      });

    },

    CanclePaymentWizard() {
      //funkcja zykająca wizzard płatność 
      let pForm = document.getElementById("PaymetWizardForm"),
        pModal = document.getElementById("PaymentWizzar");

      pForm.reset();
      pModal.style.display = "none";


    },

    

  }

};

var JsAccount = {

  GetFormEditAddress(xEvent) {
    //funkcja pokazująca formulaż do edycji danych adresowych
    //xEvent -globalny event nasłuchiwacza

    let pForm = document.getElementById(xEvent.currentTarget.dataset.target);
    pHiddeElement = document.getElementById(xEvent.currentTarget.dataset.hidden);

    if (xEvent.currentTarget.dataset.disabledprevevent !== undefined) {
      xEvent.preventDefault();
    }

    JsCommon.SetVisibleElement(undefined, pForm);
    JsCommon.HiddenElement(undefined, pHiddeElement);

  }

};
