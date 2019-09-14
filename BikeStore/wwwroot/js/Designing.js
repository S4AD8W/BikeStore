

function SetClassHideToElement(xIdElement) {
  //funkcja ustwiająca klase hidden dla id Elementu
  //xIdElement - id elementu do ukrycia

  let pItem = undefined;

  pItem = document.getElementById(xIdElement);

  if (pItem === undefined) return false;

  if (!pItem.classList.contains("hidden")) {
    pItem.classList.add("hidden");
  }

}

function SetOpacityDisableElement(xSelectedElement, xCssClass) {
  //funkcja zaciemniająca nieaktywne elementy
  //xSelectedElement - wybrany element przez użytkownika 

  let pCssClass;

  if (xCssClass === undefined) {
    pCssClass = "DisableElementDesigning";
  } else {
    pCssClass = xCssClass;
  }
  let pItems = xSelectedElement.parentElement.parentElement.children;     //kolekcja elementów z kontenera 

  for (let i = 0; i < pItems.length; i++) {
    pItems[i].classList.add(pCssClass);                     //dodanie klasy dla elementu 
  }

  xSelectedElement.parentElement.classList.remove(pCssClass);             //usuniecie klasy zaciemniającej dla wybranego Elementu przez użytkownika 

}

function DisableOpacityDisableElement(xSelectedElement, xCssClass) {
  //funkcja zaciemniająca nieaktywne elementy
  //xSelectedElement - wybrany element przez użytkownika 
  //xCssClass - klasa która ma być ustawiona

  let pCssClass;

  if (xCssClass === undefined) {
    pCssClass = "DisableElementDesigning";
  } else {
    pCssClass = xCssClass;
  }
  let pItems = xSelectedElement.parentElement.children;     //kolekcja elementów z kontenera 

  for (let i = 0; i < pItems.length; i++) {
    pItems[i].classList.remove(pCssClass);                  //dodanie klasy dla elementu 
  }

  xSelectedElement.classList.remove(pCssClass);             //usuniecie klasy zaciemniającej dla wybranego elemntu prze użytkownika 

}

function DisableOpacityDisableElements(xIdxContainer, xCssClass) {
  //funkcja zaciemniająca nieaktywne elementy
  //xSelectedElement - wybrany element przez użytkownika 
  //xCssClass - klasa która 

  let pCssClass;

  if (xCssClass === undefined) {
    pCssClass = "DisableElementDesigning";
  } else {
    pCssClass = xCssClass;
  }

  let pItems = document.getElementById(xIdxContainer).children;//kolekcja elementów z kontenera 

  for (let i = 0; i < pItems.length; i++) {
    pItems[i].classList.remove(pCssClass);                  //dodanie klasy dla elementu 
  }

}


var JsDesigning = {
  //objekt js przchowujący obiekty z funkcjami dla modułu projektowania



  Joinery: {

    IsEdit: false,                                          //czy jest w trybie edycji
    ConfirmEditAlert: "",                                   //zmiena przechowująca teść aletru dla zmiany w trybie edycji

    SetJoinery: function (xEvent) {
      //funkcja ustawiająca stolarkę 

      $("#LPProjectPattern").dxLoadPanel("instance").show();
      let pSelectedJoinery = xEvent.currentTarget.dataset.xcntwindowsystemtype,
        pSelectedElement = xEvent.currentTarget;

      $.post("SetJoinery", {
        xCntSystemType: pSelectedJoinery
      }).done(xData => {
        JsDesigning.Joinery.SelectSelectedElement(pSelectedElement); //zaciemnienie nie wybranch elementów
        if (JsCommon.CheckCurrentDesigningModule("Windows")) {  //sprawdzenie trybu projektowania na podstawie uri
          JsCommon.GoToProfileSystem();
          return true;
        }
        JsCommon.GoToOpeningSide();
      }).fail(xData => {
        debugger;
      });



    },

    SelectSelectedElement: function (xElement) {
      //funkcja ustawiająca wybrany element w projekcie okna

      let pJoineryContainer_Row = document.getElementById("JoineryRow"),
        pProfileSystem_Col = pJoineryContainer_Row.getElementsByClassName("CntSTE"),
        pSelectedCntSTE = xElement.dataset.xcntwindowsystemtype;

      JsCommon.MarkElemntAsSelected(pSelectedCntSTE, "CntSTE");



    },
  },

  Dimensions: {

    mErrorMessageLoadProjectPattern: "",                    //zmena przechowująca treść alertu dla błędu ładowania przykładowych projektów
    mId_ConstructionTypeContainer: undefined,               //zmiena przechowująca id kontenerta konstrukcji
    mIsDorDesigning: false,                                 //zmiena przechowująca informację, czy są to drzwi projektowane
    mIsEdit: false,
    mIsSibling: false,
    mIsVisibleFormDimensioningWithSvg: false,
    mSvgDimensionValue: undefined,
    mClickedTekstElementWithSvg: undefined,
    mIsDorConfiguration: false,                            //zmiena na potrzeby sprawdzenia aktulnego trybu projektowania


    pDimensionsModel: {                                     //model z danymi do komendy
      mConstructionType: undefined,
      mQuarterCount: undefined,
      mIdxProjectPattern: undefined
    },


    PrepareToView(xData) {
      //funkcja przygotowująca widok 
      //xData - przeność odpowieć serwera z wyrenderowanym widokiem 

      let pViewContainer = document.getElementById("Dimensions");

      pViewContainer.innerHTML = xData;
      $("#LPProjectPattern").dxLoadPanel("instance").hide();
      JsDesigning.Dimensions.InitializeWindowDimisioningView();
      JsCommon.GoToElemenTopView("QuarterCountContainer");

    },

    InitializeWindowDimisioningView() {
      //funkcja inicjalizująca widok czyli podpina wszstkie niezbędne funkcje dla tagów html

      document.getElementById("DimensionSvgContainer").addEventListener("keypress", JsDesigning.Dimensions.ChangeDimensionFromSvgAfterPressEnter);
      JsDesigning.Dimensions.mErrorMessageLoadProjectPattern = "@Html.Raw(cLanguages.GetText(TextEnum.CouldDoNotLoadData))";
      document.getElementById("ChangeMainDimension").addEventListener("keypress", JsDesigning.Dimensions.ChangeMainDimensions);

      var PaymentWizardForm = $("#ChangeMainDimension");

      PaymentWizardForm.validate({
        rules: {
          Width: {
            required: true
          },
          Height: {
            required: true
          },

        },

        messages: {
          Width: {
            required: "@Html.Raw(cLanguages.GetText(TextEnum.ThisFieldIsRequired))",
          },
          Height: {
            required: "@Html.Raw(cLanguages.GetText(TextEnum.ThisFieldIsRequired))"
          },
        },

        errorPlacement: function (error, element) {
          //[IW 22.08.2019] zostawiam bo może się przydać bo nigdy nie pamietam jak nawigować po elemntach domu po attr i parentach
          //if (element.attr("name") == "pdfEmailAddress") {
          //  error.insertAfter("ul.slinks");
          //  }
          //else {
          //  error.insertAfter(element);
          //}

          error.appendTo(element.parent().next());
          //error.insertBefore(element)
        }

      });

    },

    SetQuarterCount(xQuarterCount, xElement, xId_ConstructionTypeContainer) {
      //funkcja ustwiająca ile kwater wybrał użytkownik
      //xQuarteCount - liczba kwater
      //xElement- wybrany element
      //xId_ConstructionTypeContainer - id kontenera przechowującego typy konstrukcji
      debugger;
      JsDesigning.Dimensions.pDimensionsModel.mQuarterCount = xQuarterCount; //ustaienie ilości qwater
      SetOpacityDisableElement(xElement);                   //ustawienei przezroczystości dla nie wybranych Elementów

      this.SetVisibleConstructionType(xId_ConstructionTypeContainer); //pokaznie kontenera z konstrukcjami 

      if (this.mIsDorDesigning) {                           //sprawdzenie czy jest to tryb projektowania drzw
        this.pDimensionsModel.mConstructionType = undefined; //skasowanie wybranej konstrukcji
        this.HideProjectPatternContainer();                 //ukrycie przykładowych projektów
        this.DisableOpcitiDorConstructionType();            //wyłoczenie przezroczystości dla konstrukcji do wyboru
      } else {
        if (JsDesigning.Dimensions.pDimensionsModel.mConstructionType !== undefined) {//sprawdzenie czy jest wybrany typ konstrukcji
          this.GetProjectPattern();       //pobranie przykładowych projektów
        }
      }
    },

    SetConstructionType(xConstructionType, xElement) {
      //funkcja ustawiająca typ konstrukcji
      //xConstructionType - typ konstrukcji
      //xElement - wybrany element

      JsDesigning.Dimensions.pDimensionsModel.mConstructionType = xConstructionType; //przyoisanie wybranej konstrukcji
      SetOpacityDisableElement(xElement);                   //ustwienie przezroczystości dla nie wybranych konstrukcji
      this.GetProjectPattern();                            //pobranie przykładowych projektów

    },

    SetConstructionTypeForDoorWhichEmptyConstructionTypeSteps(xConstructionType, xSelectedElement) {
      //Fukcja ustawiająca typ konstrukcji dla typu drzwi 
      debugger;
      let pConstructionTypeDorOneSahs = document.getElementById("DorOneSash"),
        pConstructionTypeDorTwoSahs = document.getElementById("DorTwoSash");


      JsDesigning.Dimensions.pDimensionsModel.mQuarterCount = xSelectedElement.dataset.quartecount;
      JsDesigning.Dimensions.pDimensionsModel.mConstructionType = xConstructionType;

      JsCommon.HiddenElement(undefined, pConstructionTypeDorOneSahs);
      JsCommon.HiddenElement(undefined, pConstructionTypeDorTwoSahs);

      JsDesigning.Dimensions.GetProjectPattern();

    },

    GetProjectPattern() {
      //fuunkcja pobierająca przykładowe projekty

      $("#LPProjectPattern").dxLoadPanel("instance").show();//pokazanie loading panelu

      let xhr = new XMLHttpRequest();                       //utworzenie obiektu połoczenie
      xhr.open('POST', 'GetProjectPattern');                //otwarczie połoczenia
      xhr.setRequestHeader('Content-Type', 'application/json');//ustawienie nagłówka
      xhr.onload = function () {

        if (xhr.status === 200) {                           //sprawdzenie czy sie udało pobrać dane
          debugger;
          let pContainer = document.getElementById("ProjectsPatterns"); //pobranie kontenera na przykładowe projekty 
          pContainer.innerHTML = xhr.responseText;          //pobranie odpowiedzi z serwera i ustawienie zawrtości kontenera
          pContainer.classList.remove("hidden");            //pokazanie kontenera z paczkami projektowymi
          $("#LPProjectPattern").dxLoadPanel("instance").hide();//schowane loading panelu
        }
        if (xhr.status > 380) {                             //sprawdzenie czy cośposzło nie tak

          $("#LPProjectPattern").dxLoadPanel("instance").hide();//schowanie lading panelu

          JsDesigning.Dimensions.HideProjectPatternContainer();
          JsCommon.DXShowToast(JsDesigning.Dimensions.mErrorMessageLoadProjectPattern, undefined, JsCommon.DXToastType.Error); //wyswietlenie toasta z informacją co poszło nie tak 
        }
      };

      xhr.send(JSON.stringify({                             //zmiana obiektu z danymi na jsona
        CntQuarterCount: JsDesigning.Dimensions.pDimensionsModel.mQuarterCount,
        CntConstructionType: JsDesigning.Dimensions.pDimensionsModel.mConstructionType
      }));

    },

    SetVisibleConstructionType(xId_ConstructionTypeContainer) {
      //funkcja ustawiająca widzialnoś kontenera z typami konstrukcji 
      //xId_ConstructionTypeContainer - id kontenera z konstrukcjami 

      let pConstructionType = undefined;

      if (this.mId_ConstructionTypeContainer !== undefined) { //sprawdzenie czy w obiekcie jest ustawiony id kontenera z typem konstrukcji czyli poprzednio wybrany
        pConstructionType = document.getElementById(this.mId_ConstructionTypeContainer); //pobranie kontenera z konstrukcjami 
        if (!pConstructionType.classList.contains("hidden")) {//sprawdzenie czy porzednio wybrany kontener jest ukryty
          pConstructionType.classList.add("hidden");        //ukrycie kontenera poprzednio wybranego 
        }
      }

      this.mId_ConstructionTypeContainer = xId_ConstructionTypeContainer; //ustawienie id kontnera konstrukcji nowo wybranego 
      pConstructionType = document.getElementById(this.mId_ConstructionTypeContainer);//pobranie kontenera nowo wybranego 
      pConstructionType.classList.remove("hidden");         //pokazanie wybranego kontenera konstrukcji 

    },

    DisableOpcitiDorConstructionType() {
      //funkcja usuwająca przezroczystoć do typu konstrukcji w module projketowania drzwi 

      let pCssClas = "DisableElementDesigning";

      let pItems = document.getElementById("DorOneSash");

      for (let i = 0; i < pItems.children.length; i++) {
        pItems.children[i].classList.remove(pCssClas);                      //dodanie klasy dla elementu 
      }

      pItems = document.getElementById("DorTwoSash");

      for (let i = 0; i < pItems.children.length; i++) {
        pItems.children[i].classList.remove(pCssClas);                      //dodanie klasy dla elementu 
      }

    },

    GetSelectedProjectPattern(xSelectedElement, xSelectedIdxObject) {
      //funkcja ustawiajaca wybrany przykładowy projekt
      //xSelectedElement - wybrany element
      //xSelecteIdxObejct - index wybranego projektu 

      SetOpacityDisableElement(xSelectedElement);           //ustawienie przezroczystoś nie wybranym elementom
      this.pDimensionsModel.mIdxProjectPattern = xSelectedIdxObject; //ustawienie wybranego przykładowego projektu

      this.SendIdxProjectPattern(xSelectedIdxObject)
        .then(res => {
          debugger;
          var foo = document.getElementById("MainWidthProject");
          document.getElementById("MainWidthProject").value = res.width;
          document.getElementById("MainHeighthProject").value = res.height;
        })
        .then(res => JsDesigning.Dimensions.GetSvgStringForSelectedProjectPattern())
        .then(res => {
          let pSvgDTO = new SvgDTO(res, 1256, 655,
            "DimensionSvg", true);
          return SvgMethod.PrepareSvgToView(pSvgDTO);
        })
        .then(res => {
          InnerHtmlElementToDom("PaternImageSVG", res.SvgString);
          res.LisenerFuncionType = "click";
          res.LisenerFunctionMethod = JsDesigning.Dimensions.SetVisibleFormToChangeDiminsioning;
          return SvgMethod.AddEventListenerForSvgTextElementAboutIdProjectBar(res);
        })
        .then(res => this.GoToDimensioning(res))
        .then(() => JsCommon.RefreshLeftChoiceInfoMenu())
        .then(() => { $("#LPProjectPattern").dxLoadPanel("instance").hide(); })//schowanie loading panelu )
        .catch(error => {
          if (error.responseJSON !== undefined) {
            alert(error.responseJSON.message);            //pokazanie alertu jezelicoś posżło nie tak
            window.location.reload();                         //przeładowanie widoku
          } else {
            alert(error);
            window.location.reload();
          }
        });

    },

    SendIdxProjectPattern(xIdxProjectPattern) {
      //funkcja wysłająca indeks przykładowego projektu
      //xIdxProjectPattern - indeks przykładowego projektu 

      let pValue = {                                        //obiekt z danymi
        IdxProjectPattern: xIdxProjectPattern
      };
      $("#LPProjectPattern").dxLoadPanel("instance").show();//pokazanie loading panelu
      return new Promise((resolve, reject) => {
        $.ajax({                                              //obiekt JQ do komunikacji
          url: 'SetIdxProjectPattern',
          type: "post",
          dataType: "json",
          contentType: "application/json; charset=utf-8",
          data: JSON.stringify(pValue),
          success: function (xResponse) {
            resolve(xResponse);
          },
          error: function (xResponse) {

            throw (xResponse);
            //alert(xResponse.responseJSON.message);            //pokazanie alertu jezelicoś posżło nie tak
            //window.location.reload();                         //przeładowanie widoku
          }

        });
      });

    },

    GoToDimensioning() {
      //funkcj odpowiadająca za pokazanie użytkownikowy wymiarowania

      let pDimensioningForm = document.getElementById("FormDiminsioningPattern"),
        pSvgPatternContainer = document.getElementById("PaternImageSVG"),
        pDetailsConfigurationButton = document.getElementById("GoToDetailsConfiguration");

      pDimensioningForm.classList.remove("hidden");
      pSvgPatternContainer.parentElement.classList.remove("hidden");

      pDetailsConfigurationButton.classList.remove("hidden");
      JsCommon.GoToElemenTopView("PaternImageSVG", undefined);
    },

    GetSvgStringForSelectedProjectPattern() {
      //funkcja pobierająca rysunek svg

      return new Promise((resolve, reject) => {

        $.post("GetSvgStringCurrentProject")
          .done(function (xData) {
            resolve(xData);
          })
          .fail(function (xData) {
            reject("Ups coś poszło nie tak  ");
          });

      });
    },

    TryChangeDiminsionsFromSvg() {
      //funkcja prubująca zmienić wymiary dla projektu z rysunku svg

      $("#LPProjectPattern").dxLoadPanel("instance").show();//pokazanie loading panelu

      JsDesigning.Dimensions.CheckThePossibilityOfChangingTheDimensions()
        .then(res => JsDesigning.Dimensions.SendANewDimensionToTheServer(res))
        .then(res => JsDesigning.Dimensions.GetSvgStringForSelectedProjectPattern())
        .then(res => {
          let pSvgDTO = new SvgDTO(res, 1256, 655,
            "DimensionSvg", true);
          return SvgMethod.PrepareSvgToView(pSvgDTO);
        })
        .then(res => {
          InnerHtmlElementToDom("PaternImageSVG", res.SvgString);
          res.LisenerFuncionType = "click";
          res.LisenerFunctionMethod = JsDesigning.Dimensions.SetVisibleFormToChangeDiminsioning;
          return SvgMethod.AddEventListenerForSvgTextElementAboutIdProjectBar(res);
        })
        .then(res => JsDesigning.Dimensions.GoToDimensioning(res))
        .then(() => JsCommon.RefreshLeftChoiceInfoMenu())
        .then(res => {
          JsCommon.HiddenElement(undefined, document.getElementById("DimensionSvgContainer"));
          JsDesigning.Dimensions.mIsVisibleFormDimensioningWithSvg = false;
        })
        .then(() => { $("#LPProjectPattern").dxLoadPanel("instance").hide(); })
        .catch(reject => {

          $("#LPProjectPattern").dxLoadPanel("instance").hide();
          JsCommon.DXShowToast(reject, undefined, JsCommon.DXToastType.Error);

        });


    },

    SetVisibleFormToChangeDiminsioning(event) {
      //funkcja pokazująca input do zmiany wymiarowania projektu po kliknieciu w wymiar na svg

      let pDimensionInput = document.getElementById('DimensionInput'), //pobranie pojemnika na szerokości
        pFrmSvgDimensions = document.getElementById("DimensionSvgContainer");

      var x = event.pageX - $('#PaternImageSVG').offset().left;
      var y = event.pageY - $('#PaternImageSVG').offset().top;

      pFrmSvgDimensions.style.left = x + "px";
      pFrmSvgDimensions.style.top = y + "px";

      JsDesigning.Dimensions.mSvgDimensionValue = parseInt(event.currentTarget.innerHTML);
      JsDesigning.Dimensions.mClickedTekstElementWithSvg = event.currentTarget;

      pFrmSvgDimensions.getElementsByTagName("button")[0]
        .addEventListener("click", JsDesigning.Dimensions.TryChangeDiminsionsFromSvg);

      pFrmSvgDimensions.classList.remove('hidden');     //pokazanie formularza wymiarowania
      JsDesigning.Dimensions.mIsVisibleFormDimensioningWithSvg = true; //ustawienie zmienej informującej ze formularz jest widoczny
      pDimensionInput.value = JsDesigning.Dimensions.mSvgDimensionValue;
      pDimensionInput.focus();                              //ustawienie fokusa na formularz
      pDimensionInput.select();                             //wybranie elementu
      event.stopPropagation();                              //zatrzymianie propagacji zdarzenia
      event.preventDefault();                               //anulowanie domyślnego zdarzenia

    },

    CheckThePossibilityOfChangingTheDimensions() {
      //funkcja sprawdzająca możliwoś zmiany wymiarów dla projektu

      let pDimensionInput = document.getElementById('DimensionInput'),
        pProjectBarIdValue = undefined,
        pNewValue = parseInt(pDimensionInput.value);

      return new Promise((resolve, reject) => {
        if (JsDesigning.Dimensions.mClickedTekstElementWithSvg === undefined) reject("Nie poprawna wartoś spróbuj ponownie");
        if (JsDesigning.Dimensions.mSvgDimensionValue === parseInt(pDimensionInput.value)) {
          reject("Nie poprawna wartoś spróbuj ponownie");
        }


        for (var i = 0; i < this.mClickedTekstElementWithSvg.attributes.length; i++) {
          if (JsDesigning.Dimensions.mClickedTekstElementWithSvg.attributes[i].name === "projectbarid") {
            pProjectBarIdValue = JsDesigning.Dimensions.mClickedTekstElementWithSvg.attributes[i].nodeValue;
          }
        }

        resolve({
          ProjectBarIdValue: pProjectBarIdValue,
          Value: pNewValue
        });

      });


    },

    SendANewDimensionToTheServer(xDimensionsObject) {
      //funkcja przesłająca nowe wymiary do serwera
      //XdimensionObject - objekt przenoszący informację o nowych wymiarach

      return new Promise((resolve, reject) => {

        $.post("ChangeDimensionsFromSvg", {
          Value: xDimensionsObject.Value,
          ProjectBarId: xDimensionsObject.ProjectBarIdValue
        })
          .done(function (xData) {
            resolve(xData);
          })
          .fail(function (xData) {
            reject(xData.responseJSON);
          });
      });

    },

    ChangeDimensionFromSvgAfterPressEnter(xEvent) {
      //fukcja sprawdza czy podczas podawania wymiarów został wcisniety enter zeby sprubowąć zmienić wymiary
      //xEvent  - przeność informacje od addeventlisenera

      if (xEvent.keyCode !== jsKeyCode.Enter) return false;

      JsDesigning.Dimensions.TryChangeDiminsionsFromSvg();

    },

    ChangeMainDimensions(xEvent) {
      //funkkcja zmieniająca głowne wymiary rysunku takie jak szerokość i wysokoś
      //xEvent  - przeność informacje od addeventlisenera

      if (xEvent.keyCode !== jsKeyCode.Enter) return false;

      let pForm = xEvent.currentTarget,
        pWidth = parseInt(pForm["Width"].value),
        pHeight = parseInt(pForm["Height"].value);

      if (!$('#ChangeMainDimension').valid()) return false;

      $("#LPProjectPattern").dxLoadPanel("instance").show();//pokazanie loading panelu

      $.post("/Designing/ChangeMainDimensions", {
        Width: pWidth,
        Height: pHeight
      })
        .done(function (xData) {
          JsDesigning.Dimensions.RrefreshDataAfterChangeMainDimensionsProject();
        })
        .fail(function (xData) {
          $("#LPProjectPattern").dxLoadPanel("instance").hide();//pokazanie loading panelu
          JsCommon.DXShowToast(xData.responseText, undefined, JsCommon.DXToastType.Error);
        });

      JsCommon.RefreshLeftChoiceInfoMenu();

    },

    RrefreshDataAfterChangeMainDimensionsProject() {
      //funkcja dśierzająca dane na karcie wymiarowania po zmianie wymiarów

      JsDesigning.Dimensions.GetSvgStringForSelectedProjectPattern()
        .then(res => {
          let pSvgDTO = new SvgDTO(res, 1256, 655,
            "DimensionSvg", true);
          return SvgMethod.PrepareSvgToView(pSvgDTO);
        })
        .then(res => {
          InnerHtmlElementToDom("PaternImageSVG", res.SvgString);
          res.LisenerFuncionType = "click";
          res.LisenerFunctionMethod = JsDesigning.Dimensions.SetVisibleFormToChangeDiminsioning;
          return SvgMethod.AddEventListenerForSvgTextElementAboutIdProjectBar(res);
        })
        .then(res => JsDesigning.Dimensions.GoToDimensioning(res))
        .then(res => {
          JsCommon.HiddenElement(undefined, document.getElementById("DimensionSvgContainer"));
          JsDesigning.Dimensions.mIsVisibleFormDimensioningWithSvg = false;
        })
        .then(res => { JsCommon.RefreshLeftChoiceInfoMenu(); })
        .then(() => { $("#LPProjectPattern").dxLoadPanel("instance").hide(); })
        .catch(reject => {

          $("#LPProjectPattern").dxLoadPanel("instance").hide();
          JsCommon.DXShowToast(reject, undefined, JsCommon.DXToastType.Error);

        });

    }

  },

  ProfileSystems: {

    mErrorSetProfileSystem_Text: undefined,                 //trść komunikatu jak coś pujdze nie tak z ustawieniem systemu profilowego
    mIdxProfileSystem: undefined,                           //indeks wybranego systemu profilowego
    mEditCommandObject: undefined,
    mIsEditStep: false,
    mErrorEditStep_Text: undefined,

    SetProfileSystem(xEvent) {
      //funkcja pobierająca sustem profilowy w kontekscie pobiera z widoku
      //xIdxProfileSystem - ideks wybranego systemu profilowego
      //xElement - wybrany element

      debugger;
      let pSelectedProfileSytem = xEvent.currentTarget,
        pIdxSelectedProfileSystem = xEvent.currentTarget.dataset.idxprofilesystem;

      JsDesigning.ProfileSystems.mIdxProfileSystem = pIdxSelectedProfileSystem;
      $("#LPProjectPattern").dxLoadPanel("instance").show();

      $.post("SetProfileSystem", {
        IdxProfileSystem: pIdxSelectedProfileSystem
      })
        .done(xData => {
          debugger
          JsCommon.MarkElemntAsSelected(pIdxSelectedProfileSystem, "P_S");
          JsDesigning.ProfileSystems.PrepareToViewProjectPackage(xData);
          JsCommon.GoToElemenTopView("ProjectPackageRowInCard");
          JsCommon.RefreshLeftChoiceInfoMenu();
          $("#LPProjectPattern").dxLoadPanel("instance").hide();
        })
        .fail(xdata => {

        });
    },

    PrepareToView() {
      //funkcja przygotowująca widok do wyświetlenia

      let pProfileSystemLinks = document.getElementsByClassName("ProfileSystemLink");

      for (var i = 0; i < pProfileSystemLinks.length; i++) {
        pProfileSystemLinks[i].addEventListener("click", JsDesigning.ProfileSystems.SetProfileSystem)
      }

      JsCommon.GoToElemenTopView("ProfileColorContainer");
    },

    PrepareToViewProjectPackage(xData) {
      //funkcja przygotowująca widok do wyświetlenia z paczkami projektowymi
      //xData odpowiedż serwra z wyrenderowanym widokiem

      let pContainer = document.getElementById("ProjectPackageRowInCard"),
        pProjectPackageElements = undefined;

      pContainer.innerHTML = xData;
      pProjectPackageElements = pContainer.getElementsByClassName("DesigningItemBS");

      for (var i = 0; i < pProjectPackageElements.length; i++) {
        pProjectPackageElements[i].addEventListener("click", JsDesigning.ProfileSystems.SetProjectPackage);
      }

    },

    SetProjectPackage(xEvent) {
      //funkcja ustwiająca paczkę projektową

      let pSelectedProfileSytem = xEvent.currentTarget,
        pIdxSelectedProjectPackage = xEvent.currentTarget.dataset.idxprojectpackage;

      $("#LPProjectPattern").dxLoadPanel("instance").show();
      debugger;
      $.post("SetProjectPackage", {
        IdxProjectPackage: pIdxSelectedProjectPackage,
        IdxConfProfileSystem: JsDesigning.ProfileSystems.mIdxProfileSystem
      })
        .done(xData => {
          JsCommon.MarkElemntAsSelected(pIdxSelectedProjectPackage, "P_Package");
          JsDesigning.ProfileColors.PrepareToView(xData);
          JsCommon.RefreshLeftChoiceInfoMenu();
          $("#LPProjectPattern").dxLoadPanel("instance").hide();
        })
        .fail(xData => {

        });


    },


  },

  ProfileColors: {


    IsEdit: false,
    IsLoadData: true,
    IsChange: false,
    mErrorChangeText: undefined,
    mConfirmMessage: undefined,
    mIsSingle_EnumValue: undefined,
    mIsBiColors_EnumValue: undefined,
    mIsWhiteColor_EnumValue: undefined,
    mCommandObj: undefined,

    PrepareToView(xData) {
      //funkcja przygotowująca wyrenderowany widok do wyświetlenia
      //xData - wyrenderowany widok przez serwer

      let pContainer = document.getElementById("ProfileColorContainer"),
        pColorItems = undefined,
        pColorTypeGrup_btn = undefined;

      pContainer.innerHTML = xData;
      pColorTypeGrup_btn = pContainer.getElementsByClassName("ColorBtn");
      pColorItems = pContainer.getElementsByClassName("WindowColorItem");

      for (var i = 0; i < pColorTypeGrup_btn.length; i++) {
        pColorTypeGrup_btn[i].addEventListener("click", JsDesigning.ProfileColors.SetVisibleSelectedColorGrup);
      }

      for (var j = 0; j < pColorItems.length; j++) {
        pColorItems[j].addEventListener("click", JsDesigning.ProfileColors.SelectSelectedColor);
      }

    },

    SetVisibleSelectedColorGrup(xEvent) {
      //funkcja pokazująca wybraną grupę kolorów przez urzytkownika
      //xEvent - globalne zdarzenie z kliknietego elementu na stronie

      pElementToView = document.getElementById(xEvent.currentTarget.dataset.show),
        pImgeDropDawn = xEvent.currentTarget.getElementsByTagName("img");

      if (pElementToView.classList.contains("hidden")) {
        pElementToView.classList.remove("hidden");
        pImgeDropDawn[0].classList.add("ImgDropDownListActive");
      } else {
        pElementToView.classList.add("hidden");
        pImgeDropDawn[0].classList.remove("ImgDropDownListActive");
      }

    },

    SelectSelectedColor(xEvent) {
      //Funckaja ustawiająca wybrany kolor 
      //xEvent - globalny ewvent pokchodzący z kliknietego elemntu na stronie

      let pSelectedColor = xEvent.currentTarget,
        pIsWhiteColorSelected = false,
        pIdxSelectedColor = xEvent.currentTarget.dataset.colorid;

      if (xEvent.currentTarget.dataset.iswhite !== undefined) {
        pIsWhiteColorSelected = true;
      }

      JsDesigning.ProfileColors.SetActiveSelectColor(pSelectedColor);
      JsDesigning.ProfileColors.SendSelectedColor(pIdxSelectedColor, pIsWhiteColorSelected);
    },

    SendSelectedColor(xIdxProfileColor, xIsWhiteColor) {
      debugger;
      $("#LPProjectPattern").dxLoadPanel("instance").show();

      $.post("/Designing/SetProfileColor", {
        IdxConfProfileColor: xIdxProfileColor,
        IsWhite: xIsWhiteColor
      })
        .done(xData => {
          JsCommon.GoToDimensions();
        })
        .fail(xData => {
          debugger;
        });
    },

    SetActiveSelectColor(xElement) {
      //funkcja ustwiajająca dla wybranego alemntu klase active
      //xElement - element z wybranym kolorem

      pItems = document.getElementsByClassName("WindowColorItem"); //pobranie wyszystkich elementów z kolorami

      for (var i = 0; i < pItems.length; i++) {
        pItems[i].classList.remove('WindowColorItem-Active'); //usuniecie klasy actiw dla Elementu
      }
      xElement.classList.add('WindowColorItem-Active');       //dodanie klasy actiwe dla elementu

    },


  },

  ContextMenu: {

    MenuState: 0,                                           //informacja czy menu jest wusiwietlane
    Active: "context-menu--active",                         //klasa dla menu kontekstwego
    Menu: 0,                                                //zmiena na menu

    GetMousePosition(event) {
      //funkcja pobierająca pozycie myszki
      //event - obiekt zdarzenia

      var pOsX = 0;
      var pOsY = 0;

      if (!event) event = window.event;

      if (event.pageX || event.pageY) {
        pOsX = event.clientX;
        pOsY = event.clientY;
      } else if (event.clientX || event.clientY) {
        pOsX = event.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
        pOsY = event.clientY + document.body.scrollTop + document.documentElement.scrollTop;
      }

      return {
        x: pOsX,
        y: pOsY
      };

    },

    GetMousePositionForToViewElement(event) {
      //pobranie pozycji myszki wzglendem warstwy
      //event - obiekt zdarzenia

      var pOsX = 0;
      var pOsY = 0;

      if (!event) event = window.event;

      if (event.pageX || event.pageY) {
        pOsX = event.layerX;
        pOsY = event.layerY;
      } else if (event.clientX || event.clientY) {
        pOsX = event.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
        pOsY = event.clientY + document.body.scrollTop + document.documentElement.scrollTop;
      }

      return {
        x: pOsX,
        y: pOsY
      };

    },

    ContextMenuListener(el) {
      //funkcja odpowiadająca za przygotowanie i wyświetlenie  menu kontekstowego
      //el - element 

      var pElementsSVGPoint;
      var pArrayIdxPolygons;

      el.addEventListener("click", e => {
        JsDesigning.ContextMenu.ToggleMenuOff();            //przypisanie zdarzenia do elemenu
      });

      el.addEventListener("contextmenu", function (e) {     //przypisanie zdarzenie do element

        e.preventDefault();                                 //wyłoczenie zdarzenia domyślnego
        pElementsSVGPoint = JsDesigning.WingDesigning.GetElementsFromPoint(e); //pobranie elementów dla punktu
        pArrayIdxPolygons = JsDesigning.WingDesigning.GetElementsIdxPolygon(pElementsSVGPoint); //tablica indeksów dla punktu
        JsDesigning.ContextMenu.GetContextMenu(pArrayIdxPolygons);
        JsDesigning.ContextMenu.SetPositionContextMenu(e); //ustawienie pozycji dla menu kontekstowego
      });

    },

    ToggleMenuOff() {
      //funkcja ukrywająca menu

      if (JsDesigning.ContextMenu.MenuState === 1) {        //sprawdzenie czy menu jest widoczne
        JsDesigning.ContextMenu.MenuState = 0;              //ustawienie zmienej informującej o widoczności menu 
        JsDesigning.ContextMenu.Menu.classList.remove(this.Active); //usuniencie klasy ustawiającej widoczność menu 

      }
    },

    ToggleMenuOn() {
      //funkcja ukrywająca menu

      if (JsDesigning.ContextMenu.MenuState === 0) {        //czprawdzenie czy menu nie jest widoczne
        JsDesigning.WingDesigning.HiddenFrmDimensions();    //ukrycie formularza podawania wymiaów dla elementu svg
        JsDesigning.ContextMenu.MenuState = 1;              //ustawienie zmienej informujacej o widoczności menu 
        JsDesigning.ContextMenu.Menu.classList.add(this.Active); //dodanie klasy powodującej widoczność menu 

      }
    },

    GetContextMenu(xArrayIdxPolygons) {
      //funkcja pobierająca z serwera menu kontextowe
      //xArrayIdxPolygons - tablica indeksów poligonów

      var pUrl = '/DesigningContextMenu/prlContextMenu';
      var pData = { xPolygonIndexes: xArrayIdxPolygons };

      $.ajax({
        url: pUrl,
        data: pData,
        type: 'POST',
        async: false,
        success: function (xResult) {
          JsDesigning.ContextMenu.Menu = document.getElementById("context-menu");//pobranie kontenera na menu
          JsDesigning.ContextMenu.Menu.innerHTML = xResult; //przypisanie menu do elementu
          JsDesigning.ContextMenu.ToggleMenuOn();           //pokazanie menu
          JsDesigning.ContextMenu.AddEventLisnerForMeuItem();
        },
        error: function (xhr) {
          JsDesigning.ContextMenu.ToggleMenuOff();          //schowanie menu
        }
      });

    },

    SetPositionContextMenu(event) {
      //funkcja ustawiająca pozycje menu
      //event - obiekt zdarzenia 

      let pClickCoords = this.GetMousePosition(event),      //pobranie pozycji menu
        pContextMenu = document.getElementById('context-menu');//pobranie kontenera menu 

      var x = event.pageX - $('#SvgContainer').offset().left;
      var y = event.pageY - $('#SvgContainer').offset().top;

      pContextMenu.style.left = x + "px";
      pContextMenu.style.top = y + "px";

    },

    AddEventLisnerForMeuItem() {
      debugger;
      let pElements = document.getElementsByClassName("context-menu__link");

      for (var i = 0; i < pElements.length; i++) {
        pElements[i].addEventListener("click", JsConfiguration_WithRazor.GetSelectedOptionFromContextMenu);
      }
    }

  },

  WingDesigning: {

    Idx_CurrentSelectWing: -1,                              //IndexWybranego skrzydła
    IsEdit: false,                                          //informacja czy jest edycja 
    FrmSvgDimensions: null,                                 //formularz wymiarowania dla elementu svg
    mClickTextOnSvg: null,
    mIsVisibleFrmSvgDimensions: false,
    mSvgId: "WindowProjectSvg",
    mIdxPolygon: null,
    mIdChangeGlassType: 'WingGlasType',
    mIdChangeBeams: 'BeamsOption',
    mIdChangeAirVent: 'AirVent',
    mIsDorConfiguration: false,

    PrepareToView() {
      //funkcja przygotowójaca widok 

      JsDesigning.WingDesigning.PrepareSvg();
      JsDesigning.WingDesigning.AddEventListenerForSvgElement();
      JsDesigning.WingDesigning.AddClickSvgElementsLisener();
      document.getElementById("DimensionSvgContainer123").addEventListener("keydown", JsDesigning.WingDesigning.ResolveKeyForDimensioningFromSvg);
      document.getElementById("GoToSummary").addEventListener("click", JsCommon.GoToSummary);
      JsDesigning.WingDesigning.mIsDorConfiguration = JsCommon.CheckCurrentDesigningModule("Doors");
      document.getElementById("btnHidePopUpWingsConfiguration").addEventListener("click", JsCommon.HiddenElement);

      if (JsDesigning.WingDesigning.mIsDorConfiguration) {
        JsCommon.DownloadViewAdditives();
        JsCommon.DownloadViewPanels();
      }

      JsConfiguration_WithRazor.GetContexMenuContent();
      JsCommon.RefreshLeftChoiceInfoMenu();
    },

    RefeshView() {
      //funkcja odświerzająca widok skrzydeł 

      $.get("Wings")
        .done(xData => {
          let pWingsContainer = document.getElementById("WingsConfiguration");
          pWingsContainer.innerHTML = xData;
          $("#WingsConfigurationTab").tab('show');
          JsDesigning.WingDesigning.PrepareSvg();
          JsDesigning.WingDesigning.AddEventListenerForSvgElement();
          JsDesigning.WingDesigning.AddClickSvgElementsLisener();
          document.getElementById("DimensionSvgContainer123").addEventListener("keydown", JsDesigning.WingDesigning.ResolveKeyForDimensioningFromSvg);
          document.getElementById("GoToSummary").addEventListener("click", JsCommon.GoToSummary);
          JsDesigning.WingDesigning.mIsDorConfiguration = JsCommon.CheckCurrentDesigningModule("Doors");
          document.getElementById("btnHidePopUpWingsConfiguration").addEventListener("click", JsCommon.HiddenElement);
        
        })
        .fail(xData => {

        });

    },

    AddListenerForSabmitFormForWingsConfiguration() {
     //funkcja dodająca nasłuchiwacza wysłania frmularza dla formulrzy konfiguracji skrzydła

      let pForms = document.getElementsByClassName("CtxMenuForm");

      for (var i = 0; i < pForms.length; i++) {
        pForms[i].addEventListener("submit", JsDesigning.WingDesigning.SendFormWingsConfiguration);
      }

    },

    SendFormWingsConfiguration(xEvent) {
      //funkcja wysłająca formula z konfiguracją skrzydła 
      //xEvent - globalne zdarzenia nasłuchiwacza dla elementu

      xEvent.preventDefault();                              // w tym wpadku metoda preventDefault musi być jako pierwsza wykonana
      let pIdForm = "#" + xEvent.currentTarget.id;
      pForm = $(pIdForm),
        pFullActionUri = pForm.attr('action'),
        pActionFragments = pFullActionUri.split("/"),
        pSendFormUri = "/Designing/" + pActionFragments[2],
        pArrayForm = pForm.serializeArray(),
        pJsonForm = JSON.stringify(JsCommon.ObjectArrayToArray(pArrayForm));

      $.post(pSendFormUri, { xVM: pJsonForm })
        .done(xData => {
          debugger;
          JsCommon.GoToDetailsConfiguration();
          JsCommon.RefreshLeftChoiceInfoMenu();
        })
        .fail(xData => {

        });

      
    },

    GetSelectGlassCategory(xElement) {
      //funkcja pokazująca wybraną kategorie szyb
      //xElement - obsłygiwany element

      let pInputGlassType = xElement.getElementsByTagName("input")[0];
      let pIdGlassCategoryContainer = pInputGlassType.value;
      let pGlassCategory = document.getElementById(pIdGlassCategoryContainer);      //pobranie kontenera z wybraną kategorią
      let pContainers = document.getElementsByClassName("SelectedGlassContainer"); //pobranie wszystkich kontenerów kategorii

      pInputGlassType.checked = true;

      for (var i = 0; i < pContainers.length; i++) {        //iteracja po wszystkich kontenerach w celu ukrycia
        if (!pContainers[i].classList.contains("hidden"))
          pContainers[i].classList.add("hidden");
      }

      if (pGlassCategory !== undefined) {
        if (pGlassCategory.classList.contains("hidden")) {
          pGlassCategory.classList.remove("hidden");        //pokazanie wybranej kategorii
        }
      }

    },

    EnableSelectList(xElement) {
      //funkcja właczająca listę wyboru szyby
      //xElement - wybrana lista z szybami na podstawie klikniecia w label
      
      let pForm = document.getElementById("FormGlass");     //pobranie formularza z wyborem szyb
      let pSelectLists = pForm.getElementsByTagName("select");//pobranie wszystkich list wyboru z formularza

      for (var i = 0; i < pSelectLists.length; i++) {       //iteracja po wszystkich listach wyboru z formularza w celu wyłoczenia
        if (!pSelectLists[i].hasAttribute("disabled")) {
          pSelectLists[i].disabled = true;
        }
      }
      debugger;
      let pItem = xElement.parentNode.getElementsByTagName("select"); //wybrany element
      pItem[0].removeAttribute("disabled");                 //odblokowanie wybranej listy

    },

    SetDisableAllSelectGlassList() {
      //funkcja wyłączająca wszystkie listy wyboru i ukrywająca kontenery z rodzajami szyb

      let pForm = document.getElementById("FormGlass");       //pobranie formularza
      let pSelectedList = pForm.getElementsByTagName("select");//pobranie z formularza wszystkich list wyboru
      let pContainers = document.getElementsByClassName("SelectedGlassContainer");//pobranie wszystkich kontenerów z rodzajami szyb

      for (var i = 0; i < pSelectedList.length; i++) {        //iteracja po wszystkich listach wyboru wcelu ich wyłączenia
        pSelectedList[i].disabled = true;
      }

      for (var j = 0; i < pContainers.length; j++) {          //iteracja po wszystkich kontenerach z rodzajami szyb w celu ukrycia
        if (!pContainers[j].classList.contains("hidden"))
          pContainers[j].classList.add("hidden");
      }

    },

    RemoveWingConfiguration(xIdxPolygon, xCntRemove) {
      //funkcja usówająca wybraną konfiguracje skrzydła
      //xIdxPolygon - indeks poligonu dla którego ma być usunieta konfiguracja skrzydła 
      //xCntRemove - informacja co jest usówane

      $.ajax({
        type: "POST",
        url: "RemoveWingConfiguration",
        async: false,
        data: {
          IdxPolygon: xIdxPolygon,
          CntRemoveWingConfguration: xCntRemove,
        },
        success: function (xdata) {
          window.location.reload(true);                     //przeładowanie widoku
        },
        error: function (xMessage) {
          JsCommon.DXShowToast(xMessage.responseJSON, undefined, JsCommon.DXToastType.Error); //wyświetleniu komunikatu co poszło nie tak
        }
      });

    },

    CancleEditWings(xIdForm) {
      //funkcja anulująca wybór opcji skrzydła
      //xIdForm - indeks formularza

      JsCommon.ClosePopUp("PopupSvg");
      JsCommon.ResetForm(xIdForm);

    },

    PrepareSvg() {
      //funkcja przygotowująca rysunek svg

      let pSvgContainer = document.getElementById("SvgContainer"), //pobranie kontenera svg 
        pSvgOldStr = pSvgContainer.innerHTML,               //przyoisanie starego rysunku
        pSvg = undefined,                                   //rysunek
        pNewStrSvg = undefined;                             //nowy rysunek

      pSvg = new SvgTransformer(pSvgOldStr);                //utworzenie nowego rysunku
      pSvg.Id = this.mSvgId;                                //przypisanie id dla rysunku
      pSvg.IsCripsEdge = true;                              //zmiena mówionca czy ma być włoczone wygładzanie krawiedzi 
      pSvg.Width = pSvgContainer.clientWidth;               //ustwienie szerokości rysunku
      pSvg.Height = pSvgContainer.clientHeight;             //ustawienie wysokości rysunku

      pNewStrSvg = pSvg.GetNewSvgStr();                     //pobranie nowego rysunku

      pSvgContainer.innerHTML = pNewStrSvg;                 //wyświetlenie rysunku

    },

    AddEventListenerForSvgElement() {
      //funkcja dodająca  nasłuchiwacza zdarzeń dla elementów svg

      let pSvg = document.getElementById(this.mSvgId),      //pobranie rysunku svg
        pTextDic = pSvg.getElementsByTagName("text");       //pobranie elementów z tagiem text

      for (var pText = 0; pText < pTextDic.length; pText++) {//iteracja po elementach z tagiem text
        for (var pAtrr = 0; pAtrr < pTextDic[pText].attributes.length; pAtrr++) {//iteracja po atrybutach elementu
          if (pTextDic[pText].attributes[pAtrr].name === "projectbarid") {//sprawdzenie czy element maszukany atrybut
            pTextDic[pText].addEventListener('click', function (event) {//dodanie dla elementu nasłuchiwacza
              JsDesigning.WingDesigning.GetFrmDimensions(event); //przypisanie zdarzenia dla nasłuchiwacza
            });

          }
        }
      }

    },

    GetFrmDimensions(event) {
      //funkcja pokazująca  formularz wymiarowania
      //event - obiekt zdarzenia


      let pClickCoords = JsDesigning.ContextMenu.GetMousePositionForToViewElement(event), //kordynaty klikniecia
        pDimensionInput = document.getElementById('DimensionInputFromWingCart'), //pobranie pojemnika na szerokości
        pPath = JsCommon.GetElementPathFromEventLisener(event),
        pValue;

      JsDesigning.WingDesigning.FrmSvgDimensions = document.getElementById("DimensionSvgContainer123");

      var x = event.pageX - $('#SvgContainer').offset().left;
      var y = event.pageY - $('#SvgContainer').offset().top;

      JsDesigning.WingDesigning.FrmSvgDimensions.style.left = x + "px";
      JsDesigning.WingDesigning.FrmSvgDimensions.style.top = (y = y + 85) + "px";

      for (var pAtrr = 0; pAtrr < pPath.length; pAtrr++) { //przejscie po atrybutach kliknietego elementu
        if (pPath[pAtrr].localName === "text") {       //sprawdzenie czy atrybut jest szukanym
          this.mClickTextOnSvg = pPath[pAtrr];         //przypisanie zawartosci atrybutu 
        }
      }

      pValue = this.mClickTextOnSvg.textContent;
      JsDesigning.WingDesigning.FrmSvgDimensions.classList.remove("hidden");
      JsDesigning.WingDesigning.mIsVisibleFrmSvgDimensions = true;               //ustawienie zmienej informującej ze formularz jest widoczny
      pDimensionInput.value = parseInt(pValue);             //sparsowanie wartości na integer i pokzanie w formularzu 
      pDimensionInput.focus();                              //ustawienie fokusa na formularz
      pDimensionInput.select();                             //wybranie elementu
      event.stopPropagation();                              //zatrzymianie propagacji zdarzenia
      event.preventDefault();                               //anulowanie domyślnego zdarzenia

    },

    DisabledFrmDimensionsFromMouseClick(event) {
      //ukrycie formularza wymiarowania po kliknieciu w inny element
      //event - obiekt zdarzenia

      let pElementName = event.path[0].id;

      //IW wiem, że to głupio wygląda ale w innej formie nie chce prawidłowo działać
      if (pElementName === this.mSvgId) {                    //sprawdzenie czy klikniety element niejst elementem bierzoncym
      } else {
        if (!this.FrmSvgDimensions.classList.contains('hidden')) {//sprawdrzenie czy element niema ustaionej klasy ukrywającej 
          this.FrmSvgDimensions.classList.add('hidden');    //ukrycie formularza
        }
      }

    },

    HiddenFrmDimensions() {
      //funkcja ukrywająca formularz wymiarowania

      JsDesigning.WingDesigning.FrmSvgDimensions = document.getElementById("DimensionSvgContainer123");

      if (!this.FrmSvgDimensions.classList.contains('hidden')) {//sprawdzenie czy element ma ustawioną klase ukrywającą
        this.FrmSvgDimensions.classList.add('hidden');      //dodanie klasy ukrywającej do elementu
        this.mClickTextOnSvg = null;                        //usuniecie textu z rysunku svg
      }

    },

    TryChangeDimensionsFromTextSvg() {
      //funkjca prubująca zmienić wymiar projektu po kliknieciu w element rusunku projektu

      let pNewValue = parseInt(document.getElementById('DimensionInputFromWingCart').value), //pbranie wartości nowej
        pSvgValue;                                          //wartość z rysunku


      if (this.mClickTextOnSvg === null) {
        return false;                                       //????
      }

      SvgValue = parseInt(this.mClickTextOnSvg.textContent); //sparsowanie starej wartości na integer

      if (pSvgValue === pNewValue) {                        //porównanie starej wartości do nowej
        this.HiddenFrmDimensions();                         //ukrycie formularza
        return false;                                       //wyjście z funkcji
      }

      for (var i = 0; i < this.mClickTextOnSvg.attributes.length; i++) { //iteracja po atrybutach elementu
        if (this.mClickTextOnSvg.attributes[i].name === "projectbarid") {//sprawdzenie czy atrybut ma szukaną nazwe
          pProjectBarId = this.mClickTextOnSvg.attributes[i].value; //przypisanie watości do atrybutu 
        }
      }

      $.post("ChangeDimensionsFromSvg",
        {
          Value: pNewValue,
          ProjectBarId: pProjectBarId,
        }).done(xData => {
          JsDesigning.WingDesigning.RefreshWingsCart();
          
        })
        .fail(xData => {
          debugger;
        });

    },

    KeyPress(event) {
      //funkcja pobsługująca zdarzenia klawiatury

      switch (event.which) {                                //sprawdzenie kodu wcisinietego klawisza
        case jsKeyCode.Escape:
          this.HiddenFrmDimensions();
          JsDesigning.ContextMenu.ToggleMenuOff();
          break;
        case jsKeyCode.Enter:
          this.TryChangeDimensionsFromTextSvg();
          break;
        default:
          return false;

      }

    },

    AddClickSvgElementsLisener() {
      //funkcja dodająca słuchacza do elementu svg

      let pPathElements = [];
      let pSVG = document.getElementById(this.mSvgId);      //pobranie elementu SvgDocument

      if (pSVG !== null) {
        pPathElements = pSVG.querySelectorAll("path");      //pobranie wszystkich path z dokumentu Svg
      }

      for (var i = 0, length = pPathElements.length; i < length; i++) {
        for (let j = 0; pPathElements[i].attributes.length > j; j++) {
          if (pPathElements[i].attributes[j].name === "idxpolygon")
            JsDesigning.ContextMenu.ContextMenuListener(pPathElements[i]); //dodanie słuchacza dla elementu
        }

      }
    },

    GetElementsFromPoint(e) {
      //funkcja pobierająca elementy svg należące do punktu
      // e - event

      var pMousePosition = JsDesigning.ContextMenu.GetMousePosition(e);

      var pSvgElement = document.getElementById(this.mSvgId);

      var pDocumentElementsFromPoint = document.elementsFromPoint(pMousePosition.x, (pMousePosition.y));

      var pSVGElementsMouseClick = pDocumentElementsFromPoint.filter(e => e.nodeName === "path");

      return pSVGElementsMouseClick;

    },

    GetElementsIdxPolygon(xSvgElements) {
      //funkcja zwracająca tablicę indeksów poligonów
      //tablica elementów

      var pIdxPolygons = [];
      for (var i = 0; i < xSvgElements.length; i++) {
        if (xSvgElements[i].id === "id" || xSvgElements[i].id === "SvgDocument" || xSvgElements[i].localName === 'svg'
          || xSvgElements[i].localName === 'html' || xSvgElements[i].localName === 'body') { continue; }
        let pStrSvG = xSvgElements[i].outerHTML;
        let pIsPolygon = pStrSvG.indexOf("idxpolygon");
        if (pIsPolygon !== -1) {
          let pStrLength = pStrSvG.length;
          let pStrPolygon = pStrSvG.substring(pIsPolygon, pStrSvG.length);
          let idxPolygon = pStrPolygon.replace(/\D/g, "");
          idxPolygon = parseInt(idxPolygon);
          pIdxPolygons[pIdxPolygons.length] = idxPolygon;
        }
      }
      return pIdxPolygons;

    },

    SetVisibleDesigningPopUpContent(xElementId) {
      //funkjca pokazująca wybrany kontent dla popuoa
      //xElementId - id elementu który ma być wyświetlony
      debugger;
      let pPupContentElements = document.getElementsByClassName('PopUpContent'), //pobanie lementów  popupa
        pSetVisibleElement = document.getElementById(xElementId); //pobranie elementu, który ma być wyświetlony

      JsCommon.SetVisibleElement(undefined, document.getElementById("PopupSvg"));

      for (var i = 0; i < pPupContentElements.length; i++) { //iteracja po elementach popapu
        if (!pPupContentElements[i].classList.contains('hidden')) {//sprawdzenie czy jest ustawiana klasa ukrywająca element
          pPupContentElements[i].classList.add('hidden');   //dodanie klasy ukrywającej element
        }
      }

      pSetVisibleElement.classList.remove('hidden');        //pokazanie wybranego elementu 
    },

    SetAirVentAfterClickContainer(xElement) {
      //funkcja ustawiająca wybrany nawiewnik
      //xElement nawiewnik z elementem 

      let pInput = xElement.getElementsByTagName("input")[0];

      pInput.checked = true;

    },

    RefreshWingsCart() {

      $.get("Wings")
        .done(xData => {
          let pWingsContainer = document.getElementById("WingsConfiguration");
          pWingsContainer.innerHTML = xData;
          JsDesigning.WingDesigning.PrepareToView();
        })
        .fail(xData => {

        });

    },

    ResolveKeyForDimensioningFromSvg(xEvent) {
      //funkcja rozwiązująca obśługę klawiszy dla zmiany wymiarowania z rysunku svg
      //globalny ewent pochądzący z sfokusowanego elemntu 

      let pKey = xEvent.keyCode || xEvent.charCode;

      switch (pKey) {
        case jsKeyCode.Enter:
          JsDesigning.WingDesigning.TryChangeDimensionsFromTextSvg();
          break;
        case jsKeyCode.Escape:
          JsDesigning.WingDesigning.HiddenFrmDimensions();
          break;
      }

    },



  },

  Panels: {

    PrepareToView(xView) {
      debugger;
      let pPanelsRow = document.getElementById("PanelsRow"),
        pPanelsItem = undefined;

      pPanelsRow.innerHTML = xView;
     
      pPanelsItem = document.getElementsByClassName("DoorPanel");

      for (var i = 0; i < pPanelsItem.length; i++) {
        pPanelsItem[i].addEventListener("click", JsDesigning.Panels.SetSelectedPanel);
      }

    },

    SetIsResignAddPanel() {
      //funkcja ustwiająca informacje, że użytkownik rezygnuje z dodania panelu

      let pInputIsResign = document.getElementById("IsResignPanel"), //pobranie inputa o rezygnacji
        pDivGetPanelContainer = document.getElementById("GetPanelContainer"), //pobranie kontenera z panelami
        pDivIsResign = document.getElementById("divIsResign"); //pobranie kontenera z rezygnacją

      SetClassHideToElement("PanelsContainer");             //ukrycie kontenera z panelami

      pDivIsResign.classList.add("Border-ActiveElement");   //dodanie bordaru dla kontenera o rezygnacji
      pDivGetPanelContainer.classList.remove("Border-ActiveElement"); //usuniecie borderu z konterna wyboru paneli

      pInputIsResign.checked = true;                        //ustawienie wartości inputa na wybrany
      JsDesigning.Panels.SendSettingsPanel();

    },

    SetSelectedPanel(xEevent) {
      debugger;
      //ustaienie ze użytkownik chce dodać panel
      //xElement - klikniety element

      let pInputIdxPanel = xEevent.currentTarget.getElementsByTagName("input")[0], //pobranie elementu  inputa
        pInputIsResign = document.getElementById("IsResignPanel"), //pobranie inputa o rezygnacji
      pIdxSelectedPanel = xEevent.currentTarget.dataset.value;

      pInputIdxPanel.checked = true;                        //ustawiewuenie iputa na zaznaczony z wybranym panelem
      pInputIsResign.checked = false;                       //ustaienie inputa o rezygnacji z dodania panelu na nie wybrany

      JsCommon.MarkElemntAsSelected(pIdxSelectedPanel, "DoorPanel"); //ustawienie przezroczystości dla nie wybranych elementów 
      JsDesigning.Panels.SendSettingsPanel();
     

    },

    GetPanelContainer() {
      //pokazanie kontenera z panelami do wyboru 
      debugger;
      let pPanelsContainer = document.getElementById("PanelsContainer"), //pobranie kontenera z panelami
        pInputs = undefined,
        pDivGetPanelContainer = document.getElementById("GetPanelContainer"),//pobranie diva z informacją, że użytkownik chce dodać panel
        pInputIsResign = document.getElementById("IsResignPanel"), //pobranie inputa o rezygnacji
        pDivIsResign = document.getElementById("divIsResign"); //pobranie diva o rezygnacji

      
      pDivIsResign.classList.remove("Border-ActiveElement");//usuniecie borderu z diva o rezygnacji
      pDivGetPanelContainer.classList.add("Border-ActiveElement");//dodanie borderu dla diva o checi dodania panelu
      pInputIsResign.checked = false;                       //ustawienie inputa o rezygnacji na nie wybrany

      pInputs = pPanelsContainer.getElementsByTagName("input"); //pobranie inputów z indexami panelu

      for (var i = 0; i < pInputs.length; i++) {            //iteracja po inputach z indeksami paneli
        pInputs[i].checked = false;                         //ustawienie inputa na nie wybrany
      }

      DisableOpacityDisableElements("PanelsContainer");     //wyłączenie przeroczystości dla nie wybranych elementów

      pPanelsContainer.classList.remove("hidden");          //pokazanie kontenera z panelami 


    },

    SendSettingsPanel() {
      //funkcja wysłąjąca formulaż z wybranymi sutawieniami paneli 
      debugger;
      let pForm = $("#PanelConfigureForm"),
        pFullActionUri = pForm.attr('action'),
        pArrayForm = pForm.serializeArray(),
        pJsonForm = JSON.stringify(JsCommon.ObjectArrayToArray(pArrayForm));

      $("#LPProjectPattern").dxLoadPanel("instance").show();

      $.post(pFullActionUri, { xVM: pJsonForm })
        .done(xData => {
          JsDesigning.WingDesigning.RefeshView();
          JsCommon.RefreshLeftChoiceInfoMenu();
          $("#LPProjectPattern").dxLoadPanel("instance").hide();
        })
        .fail(xData => {

        });

    }


  },

  MosquitoNet: {

    SetTypeMosquito(xEvent) {
      //funkcja ustawiająca typ moskitery
      //xEvent - obiek zdarzenia addeventlisener

      $.post("SetTypeMosquitoFrame", { xType: xEvent.currentTarget.dataset.mosquitotype })
        .done(function (xData) {
          window.location.href = xData;
        })
        .fail(function (xData) {
          alert(xData);
        });
    },

    SetCheckedColor(xEvent) {
      //funkcja ustawiająca wybrany kolor
      //xEevent - obiek zdarzenia Elementu kliknietego

      let pSourceContainer = document.getElementById(xEvent.currentTarget.dataset.sourcecontainer),
        pImputsFromSourceContainer = pSourceContainer.getElementsByTagName("input"),
        pColorItems = pSourceContainer.getElementsByClassName("DDColorItemMosquito");

      for (let i = 0; i < pImputsFromSourceContainer.length; i++) {
        pImputsFromSourceContainer[i].checked = false;

      }

      for (let i = 0; i < pColorItems.length; i++) {
        pColorItems[i].classList.remove("DDColorItemChecked");
      }

      xEvent.currentTarget.classList.add("DDColorItemChecked");
      xEvent.currentTarget.getElementsByTagName("input")[0].checked = true;

    }
  },

  Summary: {

    PrepareView() {

    },


    GetFormChangeTotalWidth(event, xFormId, xPopupId) {
      //funkcja wyświetlająca formularz do zmiany wymiarów całkowitych 

      event.returnValue = false;                              //anulowanie zdażenia globalnego

      let pWindowWidth = document.getElementById("TotalWidth").dataset.value,
        pWindowHeight = document.getElementById("TotalHeight").dataset.value,
        pForm = document.getElementById(xFormId);

      pForm.elements["Width"].value = pWindowWidth;
      pForm.elements["Height"].value = pWindowHeight;

      JsCommon.GetPopUp(xPopupId);                          //pokazanie popupu

      pForm.elements["Width"].focus();
      pForm.elements["Width"].select();


    }
  },

  RollerShutterDimensions: {

    mSelectedPatternName: undefined,

    GetMesurmentsForProjectPattern(xEvent) {
      //funkcja pokazująca pola do wypisania wymiarów dla projektu rollety
      //xEvent - globalne zdarzenie nasłuhiwacza

      //sprawdzenie czy element został wybrany
      if (JsDesigning.RollerShutterDimensions.mSelectedPatternName === xEvent.currentTarget.dataset.target) {
        return;
      } else {
        JsDesigning.RollerShutterDimensions.mSelectedPatternName = xEvent.currentTarget.dataset.target;
      }

      //deklaracja zmiennych
      let pMesurmentsContainers = document.getElementsByClassName("mesurments"),
        pForm = document.getElementById("RollerShutterPatternMesurments"),
        pInputs = pForm.getElementsByTagName("input"),
        pCurrentPatternInput = xEvent.currentTarget.lastElementChild.getElementsByTagName("input");

      //ukrycie niepotrzebnych elemntów
      for (var i = 0; i < pMesurmentsContainers.length; i++) {
        JsCommon.HiddenElement(undefined, pMesurmentsContainers[i]);
      }
      //zblokowanie wszystkich elemntów formulażą 
      for (var j = 0; j < pInputs.length; j++) {
        JsCommon.SetDisableAtrribute(pInputs[j]);
      }
      SetOpacityDisableElement(xEvent.currentTarget);
      JsCommon.SetVisibleElement(xEvent, undefined);

      //odblokowanie aktualnie wybranych elementów formularza
      for (var k = 0; k < pCurrentPatternInput.length; k++) {
        pCurrentPatternInput[k].removeAttribute("disabled");
        if (pCurrentPatternInput[k].name === "Width") {
          pCurrentPatternInput[k].focus();
        }
      }

    }

  },

  OpeningSide: {

    PrepareView() {

      let pOpenigSideLinkElements = document.getElementsByClassName("OpeningSideLink");

      for (var i = 0; i < pOpenigSideLinkElements.length; i++) {
        pOpenigSideLinkElements[i].addEventListener("click", JsDesigning.OpeningSide.SetOpeningSide)
      }

      $("#LPProjectPattern").dxLoadPanel("instance").hide();

    },

    SetOpeningSide(xEvent) {

      let pSelectedOpenigSide = xEvent.currentTarget.dataset.openingsidetype;

      $.post("SetOpeningSide", { CntOpeningSide: pSelectedOpenigSide })
        .done(xData => {
          JsCommon.MarkElemntAsSelected(pSelectedOpenigSide, "CntOS");

          JsCommon.GoToProfileSystem();

        })
        .fail(xData => {

        });


    }

  },

  Additives: {

    PrepareToView(xView) {

      let pAdditivesRow = document.getElementById("AdditivesRow");

      pAdditivesRow.innerHTML = xView;
    }
  }

};

var JsRollerShutter = {


  PrpareViewConfiguration() {

    $("#LPProjectPattern").dxLoadPanel("instance").show();
    Promise.all([
      JsRollerShutter.DownloadViewType(),
      JsRollerShutter.DownloadViewDimensions(),
      JsRollerShutter.DownloadViewColor()
    ])
      .then(resp => {
        debugger;
        $("#LPProjectPattern").dxLoadPanel("instance").hide();
      })
      .catch(resp => {
        debugger;
        alert("Somthing went wrong");
      });


  },

  DownloadViewType() {

    return new Promise((resolve, reject) => {

      $.get("Type")
        .done(function (xData) {
          document.getElementById("TypeRow").innerHTML = xData;
          resolve(true);
        })
        .fail(function (xData) {
          reject("Ups coś poszło nie tak  ");
        });

    });
  },


  DownloadViewDimensions() {

    return new Promise((resolve, reject) => {

      $.get("Dimensions")
        .done(function (xData) {
          document.getElementById("DimensionsRow").innerHTML = xData;
          resolve(true);
        })
        .fail(function (xData) {
          reject("Ups coś poszło nie tak  ");
        });

    });

  },

  DownloadViewColor() {

    return new Promise((resolve, reject) => {

      $.get("Colors")
        .done(function (xData) {
          document.getElementById("ColorRow").innerHTML = xData;
          resolve(true);
        })
        .fail(function (xData) {
          reject("Ups coś poszło nie tak  ");
        });

    });

  }

};

var JsMosquito = {


  PrpareViewConfiguration() {

    $("#LPProjectPattern").dxLoadPanel("instance").show();
    Promise.all([
      JsRollerShutter.DownloadViewType(),
      JsRollerShutter.DownloadViewDimensions(),
      JsRollerShutter.DownloadViewColor()
    ])
      .then(resp => {
        debugger;
        $("#LPProjectPattern").dxLoadPanel("instance").hide();
      })
      .catch(resp => {
        debugger;
        alert("Somthing went wrong");
      });


  },

  DownloadViewType() {

    return new Promise((resolve, reject) => {

      $.get("Type")
        .done(function (xData) {
          document.getElementById("TypeRow").innerHTML = xData;
          resolve(true);
        })
        .fail(function (xData) {
          reject("Ups coś poszło nie tak  ");
        });

    });
  },


  DownloadViewDimensions() {

    return new Promise((resolve, reject) => {

      $.get("Dimensions")
        .done(function (xData) {
          document.getElementById("DimensionsRow").innerHTML = xData;
          resolve(true);
        })
        .fail(function (xData) {
          reject("Ups coś poszło nie tak  ");
        });

    });

  },

  DownloadViewColor() {

    return new Promise((resolve, reject) => {

      $.get("Colors")
        .done(function (xData) {
          document.getElementById("ColorRow").innerHTML = xData;
          resolve(true);
        })
        .fail(function (xData) {
          reject("Ups coś poszło nie tak  ");
        });

    });

  }

};


function formItemFocus(item) {
  if (!item) {
    console.warn('no focusable item: ', item)
    return;
  }

  var savedTabIndex = item.getAttribute('tabindex');
  item.setAttribute('tabindex', '-1');
  item.focus();
  item.setAttribute('tabindex', savedTabIndex);
}

