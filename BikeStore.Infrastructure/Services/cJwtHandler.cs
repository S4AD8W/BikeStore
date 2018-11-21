using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BikeStore.Infrastructure.DTO;
using BikeStore.Infrastructure.Settings;
using BikeStore.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;

namespace BikeStore.Infrastructure.Services {

  class cJwtHandler : IJwtHandler {

    private readonly cJwtSettings mJwtSettings;


    public cJwtHandler(cJwtSettings xJwtSettings) {
      mJwtSettings = xJwtSettings;

    }

    public cJwtDto CreateToken(Guid xUserId, string xRole) {

      var pDtateNow = DateTime.UtcNow;

      var pClaims = new Claim[] {

                new Claim(JwtRegisteredClaimNames.Sub, xUserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, xUserId.ToString()),
                new Claim(ClaimTypes.Role, xRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, pDtateNow.ToTimestamp().ToString(), ClaimValueTypes.Integer64)

      };

      var pExpires = pDtateNow.AddMinutes(mJwtSettings.ExpiryMinutes);
      var pSigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mJwtSettings.Key)),
               SecurityAlgorithms.HmacSha256);
      var jwt = new JwtSecurityToken(
          issuer: mJwtSettings.Issuer,
          claims: pClaims,
          notBefore: pDtateNow,
          expires: pExpires,
          signingCredentials: pSigningCredentials
      );

      var pToken = new JwtSecurityTokenHandler().WriteToken(jwt);

      return new cJwtDto {
        Token = pToken,
        Expires = pExpires.ToTimestamp(),
      };

    }



  }
}

