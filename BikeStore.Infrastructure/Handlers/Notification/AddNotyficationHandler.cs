using AutoMapper;
using BikeStore.core.Domain;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.ServiceNotfication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Handlers.Notification {
  public class AddNotyficationHandler : ICommandHandler<AddForkNotfication> {

    private readonly IMapper mMapper;
    private readonly IForksNotificationRepository mForksNotificationRepository;

    public AddNotyficationHandler(IMapper xMapper, IForksNotificationRepository xForksNotyficationRepository) {
      mMapper = xMapper;
      mForksNotificationRepository = xForksNotyficationRepository;
    }

    public async Task HandleAsync(AddForkNotfication xCommand) {

      ForkNotification pForkNotyfication;

      pForkNotyfication = mMapper.Map< AddForkNotfication, ForkNotification>(xCommand);
      await mForksNotificationRepository.AddForksNotification(pForkNotyfication);

    }
  }
}
