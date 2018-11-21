using Microsoft.Extensions.Caching.Memory;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Accounts;
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

namespace BikeStore.Infrastructure.Handlers.Accounts {
  class cLoginHandler : ICommandHandler<cLogin> {

    private readonly IUserService mUserService;
    private readonly IJwtHandler mJwtHandler;
    private readonly IMemoryCache mCache;
    private readonly IMessage mMessage;

    public cLoginHandler( IUserService xUserService,
                        IJwtHandler xJwtHandler, IMemoryCache xCache, IMessage xMessage) {
      mUserService = xUserService;
      mJwtHandler = xJwtHandler;
      mCache = xCache;
      mMessage = xMessage;

    }

    public async Task HandleAsync(cLogin xCommand) {

      bool pIsLogin;

      pIsLogin = await mUserService.LoginAsync(xCommand.Email, xCommand.Password);
      if (pIsLogin == true) {
        cUserDto pUser = await mUserService.GetUserAsync(xCommand.Email);
        var UserPlaimsPrincipal = SetUserPlaimsPrincipal(pUser.Role, xCommand.TokenId.ToString());
        mCache.SetJwt(xCommand.TokenId, UserPlaimsPrincipal);
        await Task.CompletedTask;
      } else {
        // TODO: Zwrucić informacje dola użytkownia o nie prawidłowych danych logowania 
        mMessage.SetMesage("invalid credetial");
        await Task.CompletedTask;
      }

      
       ClaimsPrincipal SetUserPlaimsPrincipal(string xUserRole, string xUserId) {


        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, xUserId),
                    new Claim(ClaimTypes.Role, xUserRole)
                };

        var userIdentity = new ClaimsIdentity(claims, "login");

        return new ClaimsPrincipal(userIdentity);

      }

  }
  }
}