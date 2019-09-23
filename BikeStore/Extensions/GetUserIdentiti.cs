using BikeStore.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BikeStore.Extensions {
  public static class GetUserIdentiti {

    public static (Guid UserUuid, int IdxUser, bool IsLogin) GetUserIdentities(this ClaimsPrincipal xClaimsPrincipal) {

      bool pIsLogin = false;
      int pIdxUser = 0;
      Guid pUserUuId = Guid.Empty;


      if(pIsLogin = xClaimsPrincipal.Identity.IsAuthenticated) {
        Guid.TryParse(xClaimsPrincipal.Identity.Name, out pUserUuId);
        pIdxUser = Convert.ToInt32(xClaimsPrincipal.Claims.Where(x => x.Type == CustomClaim.IdxUser)
          .Select(x => x.Value).FirstOrDefault());
          
      }

      return (UserUuid: pUserUuId, IdxUser: pIdxUser, IsLogin: pIsLogin);

    }

  }
}
