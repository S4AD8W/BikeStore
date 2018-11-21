using Microsoft.Extensions.Caching.Memory;
using BikeStore.core.Domain;
using BikeStore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BikeStore.Infrastructure.Extensions {

  public enum CacheEnum {

    ProjectPatterns,
   

  }

  public static  class CacheExtensions {
    //klasa rozszerzająca interfejs zapisu w pamięci aplikacji 

   
    public static void SetJwt(this IMemoryCache xCache, Guid xUserID, ClaimsPrincipal  xUserClaimsPrincipal) //funkcja zapisująca token w pamieci na 10 s
      => xCache.Set(GetUuserClaims(xUserID), xUserClaimsPrincipal, TimeSpan.FromSeconds(10));

    public static ClaimsPrincipal GetUserClaimsPrincipal(this IMemoryCache xCache, Guid xUserID)
      => xCache.Get<ClaimsPrincipal>(GetUuserClaims(xUserID));          //funkcja odczytująca token 


    private static string GetUuserClaims(Guid xUserID)
      => $"{xUserID}-jwt";
    

  }
}
