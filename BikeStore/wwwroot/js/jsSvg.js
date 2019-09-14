class SvgTransformer {
  constructor(xSvgStr) {
    this.SvgStr = xSvgStr;//string zawiarająca wejściowy svg
    this.Id = '';//nowe Id
    this.Width = 0;//szerokość
    this.Height = 0;//wysokość
    this.IsCripsEdge = false;//znacznik ustaweiania atrybutu CripsEdge
    this.IsProjectResizer = false;//znacznik pokazania symoblu wymiaru 
    this.IsPreviewImage = false;
  }

  IsEmpty() {
    //funkcja sprawdzająca czy string z Svg jest pusty 

    return this.SvgStr === '';

  }

  IsSizeInitialized() {
    //funkcja sprawdzajaca czy rozmiar jest podany

    return this.Width > 0 && this.Height > 0;

  }

  GetNewSvgStr() {
    //funkcja tworząca nowy string Svg - główna funkcja 

    var pSvgStr = this.SvgStr;

    pSvgStr = this.TrySetNewId(pSvgStr);//ustawienie nowego id
    pSvgStr = this.TrySetCrispEdges(pSvgStr);//ustawienie atrybutu crisp edges
    pSvgStr = this.TrySetAsProjectResizer(pSvgStr);//ustawienie jako rysunku w module wymiarowania
    pSvgStr = this.ScaleByNewSize(pSvgStr);//skalowanie dla nowych wymiarów
    pSvgStr = this.SetAsPreviewImage(pSvgStr);//ustawienia jako rysunku podglądu

    return pSvgStr;

  }

  TrySetCrispEdges(xStrSvg) {
    //funkcja próbująca ustawić wartość cripsEdes atrybutu shape-rendering

    //wartość ta wymusza linie o szerokość 1 px

    //shape-rendering="xxx" -> shape-rendering="crispedges"  
    if (!this.IsCripsEdge) return xStrSvg;

    var pRegEx = new RegExp('shape-rendering="(.*?)"');
    var pOut = xStrSvg.replace(pRegEx, 'shape-rendering="crispedges"');

    return pOut;

  }

  TrySetAsProjectResizer(xStrSvg) {
    //funkcja próbująca ustawić symbol wymiaru dla tekstów

    if (!this.IsProjectResizer) return xStrSvg;

    var pArray = this.MatchTextSvg(xStrSvg);
    if (pArray === null) return xStrSvg;

    var pNewStr = xStrSvg;

    var pNewText = '';
    var pMatch = '';
    var pIdx = 0;
    for (pIdx in pArray) {
      pMatch = pArray[pIdx];
      if (!this.IsTypeProjectBar(pMatch)) {
        pNewStr = pNewStr.replace(pMatch, '');
      } else {
        pNewText = this.InsertDimensionSymbolInText(pMatch);
        pNewStr = pNewStr.replace(pMatch, pNewText);
      }
    }

    return pNewStr;

  }

  SetAsPreviewImage(xStrSvg) {
    //funkcja ustawiająca rysunek svg jako rysunek podglądu (bez tekstów i belek)

    if (!this.IsPreviewImage) return xStrSvg;

    var pNewStr = this.SetMainWidthAndHeight(xStrSvg);
    pNewStr = this.RemoveTextes(pNewStr);
    pNewStr = this.RemoveBars(pNewStr);

    return pNewStr;

  }

  SetMainWidthAndHeight(xStrSvg) {
    //funkcja ustawiająca szerokość i wysokość

    var pWidthOldVal = this.TryGetAttributeValue(xStrSvg, 'width');
    var pHeightOldVal = this.TryGetAttributeValue(xStrSvg, 'height');

    var pWidthOld = 'width="' + pWidthOldVal + '"';
    var pWidthNew = 'width="' + this.Width + '"';
    var pNewStr = xStrSvg.replace(pWidthOld, pWidthNew);

    var pHeightOld = 'height="' + pHeightOldVal + '"';
    var pHeightNew = 'height="' + this.Height + '"';
    pNewStr = pNewStr.replace(pHeightOld, pHeightNew);

    return pNewStr;

  }

  RemoveTextes(xStrSvg) {
    //funkcja usuwająca obiekty svg będące tekstami i belkami

    var pNewStr = xStrSvg;

    var pArray = this.MatchTextSvg(xStrSvg);
    var pMatch = '';
    var pIdx = 0;
    for (pIdx in pArray) {
      pMatch = pArray[pIdx];
      pNewStr = pNewStr.replace(pMatch, '');
    }

    //pArray = this.MatchPathSvg(pNewStr); 
    //for (pIdx in pArray) {
    //  pMatch = pArray[pIdx];
    //  if (this.IsTypeProjectBar(pMatch)){
    //    pNewStr = pNewStr.replace(pMatch, '');
    //  }
    //}

    return pNewStr;


  }

  RemoveBars(xStrSvg) {

    var pNewStr = xStrSvg;

    var pMatch = '';
    var pIdx = 0;

    var pArray = this.MatchPathSvg(pNewStr);
    for (pIdx in pArray) {
      pMatch = pArray[pIdx];
      if (this.IsTypeProjectBar(pMatch)) {
        pNewStr = pNewStr.replace(pMatch, '');
      }
    }

    return pNewStr;

  }

  TrySetNewId(xStrSvg) {
    //funkcja próbujaca ustawić nowe id dokumentu svg

    if (this.Id === '') return xStrSvg;

    var pMatch = this.MatchSvgHeader(xStrSvg);
    if (pMatch === null || pMatch.length < 1) return '';
    var pHeader = pMatch[0];

    var pIdValue = this.TryGetAttributeValue(pHeader, 'id');

    var pOld = 'id="' + pIdValue;
    var pNew = 'id="' + this.Id;

    var pNewStrSvg = xStrSvg.replace(pOld, pNew);

    return pNewStrSvg;

  }

  ScaleByNewSize(xStrSvg) {
    //funkcja skalujaca svg (zmiana wymiarów) dla nowego rozmiaru canvasu 

    if (!this.IsSizeInitialized()) return xStrSvg;

    var pNewStrSvg = this.ReplaceViewBoxSize(xStrSvg);

    var pTransformRule = this.CreateTransformRule();

    pNewStrSvg = this.TransformPaths(pNewStrSvg, pTransformRule);
    pNewStrSvg = this.TransformTextes(pNewStrSvg, pTransformRule);

    return pNewStrSvg;

  }


  InsertDimensionSymbolInText(xStr) {
    //funckja dodająca symbol wymiaru dla tekstów z wymiarami na belce

    var pSymbol = this.TryGetAttributeValue(xStr, 'ProjectBarTextPrefix');
    if (pSymbol === '') return xStr;

    var pText = this.TryGetInnerText(xStr);
    if (pText === '') return xStr;

    var pNewText = pSymbol + ' = ' + pText;
    var pOut = xStr.replace('>' + pText + '<', '>' + pNewText + '<');

    return pOut;

  }

  MatchSvgHeader(xStr) {
    //funkcja zwracajaca wyniki z elemetami svg typu "text"

    var pRegEx = new RegExp('<svg(.*?)>', 'g');

    return xStr.match(pRegEx);

  }

  MatchTextSvg(xStr) {
    //funkcja zwracajaca wyniki z elemetami svg typu "text"

    //var pRegEx = new RegExp('<text([\\s\\S]*?)<\/text>', 'g');
    // [AL 05.12.2018] za pomocą literału, jeżeli jest wyrażenie regularne stałe, to taki zapis jet lepszy
    var pRegEx = /<text([\s\S]*?)<\/text>/g;

    return xStr.match(pRegEx);

  }

  MatchPathSvg(xStr) {
    //funkcja zwracajaca wyniki z elemetami svg typu "path"

    var pRegEx = new RegExp('<path(.*?)( \/>|<\/path>)', 'g');

    return xStr.match(pRegEx);

  }

  IsTypeText(xStr) {
    //funkcja sprawdzająca czy dany obiekt svg jest typu tekst

    return this.ContainsTypeLetter(xStr, 't');

  }

  IsTypeProjectBar(xStr) {
    //funkcja sprawdzająca czy dany obiekt svg jest typu belka projektu

    return this.ContainsTypeLetter(xStr, 'p');

  }

  ContainsTypeLetter(xStr, xLetter) {
    //funkcja sprawdzająca czy dany element svg jest danego typu (na bazie litery danego typu)

    var pAttr = this.TryGetAttributeValue(xStr, 'types');

    var pVal = pAttr.includes(xLetter);

    return pVal;

  }

  TryGetAttributeValue(xStr, xAtr) {
    //funkcja zwracająca wartość atrybutu 

    var pRegEx = new RegExp(' ' + xAtr + '="(.*?)"', '');

    return this.MatchFirstOccurenceSafeInsdieMatch(xStr, pRegEx);

  }

  TryGetInnerText(xStr) {
    //funkcja zwracająca wewnętrzny text dla elementu text svg 

    var pRegEx = new RegExp('>(.*?)<\/text>', '');

    return this.MatchFirstOccurenceSafeInsdieMatch(xStr, pRegEx);

  }

  GetNumberFromString(xStr) {
    //funkcja zwracająca numer ze stringa

    var pRegEx = new RegExp('\\d+', '');

    var pMatch = xStr.match(pRegEx);

    if (pMatch === null || pMatch.length < 1) return '';

    return pMatch[0];

  }

  MatchFirstOccurenceSafeInsdieMatch(xStr, xRegEx) {
    //funkcja zwracająca pierwszy pasujący wynik z RegEx 

    var pMatch = xStr.match(xRegEx);

    if (pMatch === null || pMatch.length < 2) return '';

    return pMatch[1];

  }

  GetViewBoxValue(xSvgStr) {
    //funkcja zwracająca wartość atrybutu viewBox

    return this.TryGetAttributeValue(xSvgStr, 'viewBox');

  }

  GetPathDataValue(xStrPath) {
    //funkcja zwracająca wartość współrzędnych elementu path 
    //xStrPath - string svg zawierajacy element path

    return this.TryGetAttributeValue(xStrPath, 'd');

  }

  GetViewBoxSize() {
    //funkcja zwracająca rozmiar ViewBox'a (oryginalny rozmiar svg)

    var pViewBox = this.GetViewBoxValue(this.SvgStr);
    var pSplit = pViewBox.split(',');
    var pWidth = parseInt(pSplit[2]);
    var pHeight = parseInt(pSplit[3]);

    return {
      Width: pWidth,
      Height: pHeight
    };

  }

  ReplaceViewBoxSize(xStrSvg) {
    //funkcja podmieniająca wartość atrybutu viewBox na nowe wymiary

    var pViewBoxVal = this.GetViewBoxValue(xStrSvg);

    //viewBox="0, 0, 1000, 1000" 
    var pOldViewBox = 'viewBox="' + pViewBoxVal + '"';
    var pNewViewBox = 'viewBox="0, 0, ' + this.Width + ', ' + this.Height + '"';

    var pNewStrSvg = xStrSvg.replace(pOldViewBox, pNewViewBox);

    return pNewStrSvg;

  }

  CalculateTransformationScale() {
    //funkcja obliczająca skalę transformacji dla nowych wymiarów

    var pSizeOld = this.GetViewBoxSize();
    var pScaleWidth = this.GetRoundTwo(this.Width / pSizeOld.Width);
    var pScaleHeight = this.GetRoundTwo(this.Height / pSizeOld.Height);

    if (pScaleWidth < pScaleHeight) {
      return pScaleWidth;
    }

    return pScaleHeight;

  }

  CreateTransformRule() {
    //funkcja tworząca obiekt z regułami transformacji

    var pSizeOld = this.GetViewBoxSize();
    var pScale = this.CalculateTransformationScale();
    var pNewWidth = pSizeOld.Width * pScale;
    var pNewHeight = pSizeOld.Height * pScale;

    var pXOffset = Math.round((this.Width - pNewWidth) / 2);
    var pYOffset = Math.round((this.Height - pNewHeight) / 2);

    return {
      Scale: pScale,
      XOffset: pXOffset,
      YOffset: pYOffset
    };

  }

  //transformer
  TransformPaths(xStrSvg, xTransformRule) {
    //funkcja transformująca elementy typu path

    var pNewStrSvg = xStrSvg;

    var pArray = this.MatchPathSvg(xStrSvg);

    var pIdx = 0;
    var pNewText = '';

    var pMatch = '';
    for (pIdx in pArray) {
      pMatch = pArray[pIdx];
      pNewText = this.TransformPath(pMatch, xTransformRule);
      pNewStrSvg = pNewStrSvg.replace(pMatch, pNewText);
    }

    return pNewStrSvg;

  }

  TransformTextes(xStrSvg, xTransformRule) {
    //funkcja transformująca elementy typu text

    var pNewStrSvg = xStrSvg;

    var pArray = this.MatchTextSvg(xStrSvg);

    var pIdx = 0;
    var pNewText = '';

    var pMatch = '';
    for (pIdx in pArray) {
      pMatch = pArray[pIdx];
      pNewText = this.TransformText(pMatch, xTransformRule);
      pNewStrSvg = pNewStrSvg.replace(pMatch, pNewText);
    }

    return pNewStrSvg;

  }

  //path
  TransformPath(xStrPath, xTransformRule) {
    //funkcja transformująca element typu path

    var pPathData = this.GetPathDataValue(xStrPath);

    var pNewPathData = this.TransformPathData(pPathData, xTransformRule);

    var pNewStrPath = xStrPath.replace(pPathData, pNewPathData);

    //d="M50 997  50 985 "

    return pNewStrPath;

  }

  TransformPathData(xStr, xTransformRule) {
    //funkcja transformująca element typu path

    var pSplit = xStr.split('  ');//dwie spacje rozdzielają punkty 

    var pNewArray = pSplit.map((pStr) => this.TransformedPathPoint(pStr, xTransformRule));

    var pNewPathData = pNewArray.join('  ');

    return pNewPathData;

  }

  TransformedPathPoint(xStrPt, xTransformRule) {
    //funkcja transformująca element typu path

    if (xStrPt.includes('A')) return this.TransformPathPointArc(xStrPt, xTransformRule);

    return this.TransformedPathPointLine(xStrPt, xTransformRule);

  }

  TransformedPathPointLine(xStrPt, xTransformRule) {
    //funkcja transformująca element typu path - zmiana punktu ścieżki dla boku będącego prostą

    var pSplit = this.SplitPathPoint(xStrPt);
    if (pSplit.length !== 2) return xStrPt;

    var pStrX = pSplit[0];
    var pStrY = pSplit[1];

    var pOldX = this.GetNumberFromString(pStrX);
    var pNewX = this.GetTransformedX(pOldX, xTransformRule);
    var pNewStrX = pStrX.replace(pOldX, pNewX);

    var pOldY = this.GetNumberFromString(pStrY);
    var pNewY = this.GetTransformedY(pOldY, xTransformRule);
    var pNewStrY = pStrY.replace(pOldY, pNewY);

    return pNewStrX + ' ' + pNewStrY;

  }

  TransformPathPointArc(xStrPt, xTransformRule) {
    //funkcja transformująca element typu path - zmiana punktu ścieżki dla boku będącego łukiem

    var pSplit = this.SplitPathPoint(xStrPt);
    if (pSplit.length !== 7) return xStrPt; //zawsze 7 wartości

    //0 i 1 to promień, 5 i 6 to x i y, reszta bez zmian
    var pNewRadius = this.GetTransformedLength(pSplit[1], xTransformRule);
    var pNewX = this.GetTransformedX(pSplit[5], xTransformRule);
    var pNewY = this.GetTransformedY(pSplit[6], xTransformRule);

    //A4 4 0 0 0 486 856
    var pNewStr = 'A' + pNewRadius + ' ' + pNewRadius + ' '
      + pSplit[2] + ' ' + pSplit[3] + ' ' + pSplit[4] + ' '
      + pNewX + ' ' + pNewY;

    return pNewStr;

  }

  SplitPathPoint(xStr) {
    //funckja rozdzilająca stringa danego punktu w elemencie typu path

    return xStr.trim().split(' ');

  }

  //text 
  TransformText(xStrText, xTransformRule) {
    //funkcja transforumjąca element typu text 

    //<text x="829" y="836" font-family="Arial" font-size="10pt" style="fill:#000000;" >1.2</text> 

    var pNewStrText = this.TransformTextPoint(xStrText, xTransformRule);
    pNewStrText = this.TransformTextRotate(pNewStrText, xTransformRule);

    return pNewStrText;

  }

  TransformTextPoint(xStrText, xTransformRule) {
    //funkcja transforumjąca element typu text - zmiana wartości x i y

    //<text x="829" y="836" >1.2</text> 

    var pValX = this.TryGetAttributeValue(xStrText, 'x');
    var pValY = this.TryGetAttributeValue(xStrText, 'y');

    var pNewValX = this.GetTransformedX(pValX, xTransformRule);
    var pNewValY = this.GetTransformedY(pValY, xTransformRule);

    var pOldStrX = 'x="' + pValX + '"';
    var pOldStrY = 'y="' + pValY + '"';

    var pNewStrX = 'x="' + pNewValX + '"';
    var pNewStrY = 'y="' + pNewValY + '"';

    var pNewStrText = xStrText.replace(pOldStrX, pNewStrX);
    pNewStrText = pNewStrText.replace(pOldStrY, pNewStrY);

    return pNewStrText;


  }

  TransformTextRotate(xStrText, xTransformRule) {
    //funckja transformująca element typu text - zmiana wartości rotate dla nowej skali

    var pRotateVal = this.TryGetAttributeValue(xStrText, 'transform');

    //rotate(-90, 993, 500) -> -90, 993, 500
    pRotateVal = pRotateVal.replace('rotate(', '').replace(')', '');

    //wartość rotate musi mieć trzy liczby po przecinku
    var pSplit = pRotateVal.trim().split(', ');
    if (pSplit.length !== 3) return xStrText;

    var pAngle = pSplit[0];
    var pValX = pSplit[1];
    var pValY = pSplit[2];

    var pNewValX = this.GetTransformedX(pValX, xTransformRule);
    var pNewValY = this.GetTransformedY(pValY, xTransformRule);

    var pOldStr = 'rotate(' + pAngle + ', ' + pValX + ', ' + pValY + ')';
    var pNewStr = 'rotate(' + pAngle + ', ' + pNewValX + ', ' + pNewValY + ')';

    var pNewStrText = xStrText.replace(pOldStr, pNewStr);

    return pNewStrText;

  }


  GetTransformedX(xVal, xTransformRule) {
    //funkcja zwracająca nową współrzędną X

    return this.GetTransformedValue(xVal, xTransformRule.Scale, xTransformRule.XOffset);

  }

  GetTransformedY(xVal, xTransformRule) {
    //funkcja zwracająca nową współrzędną Y

    return this.GetTransformedValue(xVal, xTransformRule.Scale, xTransformRule.YOffset);

  }

  GetTransformedLength(xVal, xTransformRule) {
    //funkcja zwracająca odległość w nowej skali

    return this.GetTransformedValue(xVal, xTransformRule.Scale, 0);

  }

  GetTransformedValue(xVal, xScale, xOffset) {
    //funkcja zwracająca nową współrzędną 

    var pVal = Math.round(xVal * xScale);

    return pVal + xOffset;

  }

  GetRoundTwo(xNumber) {
    //funkcja zwracajaca liczbę zaokrągloną do 2 miejsc po przecinku

    return xNumber.toFixed(2);

  }

}
