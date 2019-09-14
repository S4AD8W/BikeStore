class SvgDTO {

  LisenerFuncionType = "";
  LisenerFunctionMethod = "";

  constructor(xSvg, xWidth, xHeight, xSetId, xIsCripsEdge) {
    this.SvgString = xSvg;
    this.Width = xWidth;
    this.Height = xHeight;
    this.Id = xSetId;
    this.IsCripsEdge = xIsCripsEdge;
  }
}


var SvgMethod = {

  PrepareSvgToView(xSvgDTO) {

    pSvg = new SvgTransformer(xSvgDTO.SvgString);         //utworzenie nowego rysunku
    pSvg.Id = xSvgDTO.Id;                                         //przypisanie id dla rysunku
    pSvg.IsCripsEdge = xSvgDTO.IsCripsEdge;                              //zmiena mówionca czy ma być włoczone wygładzanie krawiedzi 
    pSvg.Width = parseInt(xSvgDTO.Width);                     //ustwienie szerokości rysunku
    pSvg.Height = parseInt(xSvgDTO.Height);                   //ustawienie wysokości rysunku

    xSvgDTO.SvgString = pSvg.GetNewSvgStr();                     //pobranie nowego rysunku

    return new Promise((resolve, reject) => {
      resolve(xSvgDTO);
    });

  },

  AddEventListenerForSvgTextElementAboutIdProjectBar(xSvgDTO) {

    let pSvg = document.getElementById(xSvgDTO.Id),      //pobranie rysunku svg
      pTextDic = pSvg.getElementsByTagName("text");       //pobranie elementów z tagiem text

    for (var pText = 0; pText < pTextDic.length; pText++) {//iteracja po elementach z tagiem text
      for (var pAtrr = 0; pAtrr < pTextDic[pText].attributes.length; pAtrr++) {//iteracja po atrybutach elementu
        if (pTextDic[pText].attributes[pAtrr].name === "projectbarid") {//sprawdzenie czy element maszukany atrybut
          pTextDic[pText].addEventListener(xSvgDTO.LisenerFuncionType, function (event) {//dodanie dla elementu nasłuchiwacza
            xSvgDTO.LisenerFunctionMethod(event); //przypisanie zdarzenia dla nasłuchiwacza
          });

        }
      }
    }

    return new Promise((resolve, reject) => {
      resolve(xSvgDTO);
    });

  }

};



function InnerHtmlElementToDom(xIdContainerElement, xElementToInner) {

  let pContainer = document.getElementById(xIdContainerElement);

  pContainer.innerHTML = xElementToInner;

}
