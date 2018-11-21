using BikeStore.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Services {
  interface IJwtHandler {

    cJwtDto CreateToken(Guid userId, string role);

  }
}

