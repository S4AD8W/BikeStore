using BikeStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BikeStore.ViewsComponent {
  

    [ViewComponent(Name = "UserViewsManager")]
    public class UserViewsManager : ViewComponent {

      private readonly IHttpContextAccessor mHttpContextAccessor; //kontekstu akutalnego requesta

      public UserViewsManager(IHttpContextAccessor xhttpContextAccessor) {
        mHttpContextAccessor = xhttpContextAccessor;
      }


      public async Task <IViewComponentResult> InvokeAsync() {
        // funkcja zwracająca widok dla kompontetu 

        AplicationUser pAplicationUser;
        var pUserIdentity = mHttpContextAccessor.HttpContext.User; //pobranie użytkownika z kontekstu http
        string pViewName;

        if (pUserIdentity.Identity.IsAuthenticated == true) { //sprawdzenie czy jest zalogowany 
          pAplicationUser = new AplicationUser() {
            Name = pUserIdentity.Claims.Where(x => x.Type == ClaimTypes.GivenName)
                                       .Select(x => x.Value).SingleOrDefault(), //pobranie nazwy użytkownika
            Surname = pUserIdentity.Claims.Where(x => x.Type == ClaimTypes.Surname)
                                       .Select(x => x.Value).SingleOrDefault(),//pobranie nazwiska użytkownika
          };
          
          pViewName = "prlUserLogin";
        
          return  View(pViewName, pAplicationUser);            //zwrucenie widokoku dla komponentu

        }

        return  View("prlUserLogOut");                         //zwrucenie widokoku dla komponentu

      }

    }
  }

