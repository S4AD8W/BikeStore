function SetTypeColor(xValue, xIdFrm) {

  let pForm = document.getElementById(xIdFrm),
    pColorType_Lable = document.getElementById('ColorType'),
    pIsBiColorLable = document.getElementById('IsBiColorLable'),
    pIsWhiteLable = document.getElementById('IsWhiteLable');

  debugger;
  switch (xValue.name) {

    case "IsWhite":
      if (xValue.checked) {
        pForm["IsBiColor"][0].disabled = true;
        pIsBiColorLable.classList.add('Hidden');
        pColorType_Lable.classList.add("Hidden");
        pForm["IsBiColor"][0].checked = false;
        pForm["TypeColor"][1].selected = true;
      } else {
        pForm["IsBiColor"][0].disabled = false;
        pIsBiColorLable.classList.remove('Hidden');
        pColorType_Lable.classList.remove("Hidden");
      }
      break;
    case "IsBiColor":
      if (xValue.checked) {
        pForm["IsWhite"][0].disabled = true;
        pIsWhiteLable.classList.add('Hidden');
      } else {
        pForm["IsWhite"][0].disabled = false;
        pIsWhiteLable.classList.remove('Hidden');
      }
      break;

  }


}
  var JsConfiguration = {

    Common: {
      mDeleteConfirmMessage: undefined,

      ShowConfirmDialog_DeleteElement(event) {
        //funkcja wyświetlająca okienko dialogow do potwierdenia usuniecia elementu
        //event - obiekt globalnego zdarzenia 

        if (!confirm(this.mDeleteConfirmMessage)) {
          event.preventDefault();
        }
      }

    }

  };
