using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.ServiceNotfication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers.Users {
  class CreatForkNotficationHandler : ICommandHandler<AddForkNotfication> {



    public Task HandleAsync(AddForkNotfication xCommand) {
      throw new NotImplementedException();
    }
  }
}
