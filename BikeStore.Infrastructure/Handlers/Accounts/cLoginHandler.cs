using Microsoft.Extensions.Caching.Memory;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BikeStore.Infrastructure.DTO;
using BikeStore.Infrastructure.Services.Messages;
using BikeStore.Infrastructure.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BikeStore.Infrastructure.Commands.Users;
using BikeStore.Infrastructure.Types;

namespace BikeStore.Infrastructure.Handlers.Accounts {
  class cLoginHandler : ICommandHandler<LogIn> {

    private readonly IMemoryCache mCache;
    private readonly IJwtHandler mJwtHandler;
    private readonly IMessage mMessage;
    private readonly IUserService mUserService;

    public cLoginHandler(IUserService xUserService,
                        IJwtHandler xJwtHandler, IMemoryCache xCache, IMessage xMessage) {
      mUserService = xUserService;
      mJwtHandler = xJwtHandler;
      mCache = xCache;
      mMessage = xMessage;

    }

    public async Task<CommandResult> HandleAsync(LogIn xCommand) {

      bool pIsLogin;
      CommandResult pResult = new CommandResult();
      pIsLogin = await mUserService.LoginAsync(xCommand.Email, xCommand.Password);
      if (pIsLogin == true) {
        UserDTO pUser = await mUserService.GetUserDTOAsync(xCommand.Email);
        var pUserPlaimsPrincipal = SetUserClaimsPrincipal(pUser.Role, pUser.Id.ToString(), pUser.Name, pUser.Surname, pUser.IdxUser, pUser.Email);
        mCache.SetUserClaims(pUser.Id, pUserPlaimsPrincipal);
        pResult.UserId = pUser.Id;
        await Task.CompletedTask;
      } else {
        pResult.SetFailure("");
        mMessage.SetMesage(Languages.GetText(TextEnum.TheUsernameordidnotmatchPleaseTryAgain));
        await Task.CompletedTask;

      }
      return pResult;
    }

    ClaimsPrincipal SetUserClaimsPrincipal(string xUserRole, string xUserId, string xUserName, string xUserSurname, int xIdxUser, string xEmail) {


      var claims = new List<Claim>
              {
                    new Claim(ClaimTypes.Name, xUserId),
                    new Claim(ClaimTypes.Role, xUserRole),
                    new Claim(ClaimTypes.GivenName, xUserName),
                    new Claim(ClaimTypes.Surname, xUserSurname),
                    new Claim(ClaimTypes.Email, xEmail),
                    new Claim(CustomClaim.IdxUser, xIdxUser.ToString())
                };

      var userIdentity = new ClaimsIdentity(claims, "login");

      return new ClaimsPrincipal(userIdentity);

    }

  }
}
