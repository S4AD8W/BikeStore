using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Extensions {
  public static class HtmlIsSelected {

    public static string IsSelected(this IHtmlHelper xHtmlHelper, string xControllers, string xActions, string xCssClass = "selected") {

      string pCurrentAction = xHtmlHelper.ViewContext.RouteData.Values["action"] as string;
      string pCurrentController = xHtmlHelper.ViewContext.RouteData.Values["controller"] as string;

      xActions.Replace(" ", String.Empty);
      xControllers.Replace(" ", String.Empty);
      IEnumerable<string> pAcceptedActions = (xActions ?? pCurrentAction).Split(',');
      IEnumerable<string> pAcceptedControllers = (xControllers ?? pCurrentController).Split(',');

      return pAcceptedActions.Contains(pCurrentAction) && pAcceptedControllers.Contains(pCurrentController) ?
          xCssClass : String.Empty;

    }

  }
}
