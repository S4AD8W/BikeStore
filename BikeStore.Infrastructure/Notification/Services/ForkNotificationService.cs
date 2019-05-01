using AutoMapper;
using BikeStore.core.Domain.Notification;
using BikeStore.core.Domain.Notification.Repository;
using BikeStore.Infrastructure.Notification.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Notification.Services {
  public class ForkNotificationService : IForkNotificationService {

    private readonly IForkNotyficationRepository mForkNotyificationRepository;
    private readonly IForkNotyificationImagesRepository mForkNotyificationImagesRepository;
    private readonly IMapper mMapper;

    public ForkNotificationService(IForkNotyficationRepository xForkNotyificationRepository, IForkNotyificationImagesRepository xForkNotyificationImagesRepository,
      IMapper xMapper) {
      mForkNotyificationImagesRepository = xForkNotyificationImagesRepository;
      mForkNotyificationRepository = xForkNotyificationRepository;
      mMapper = xMapper;
    }

    public async Task<bool> AddForkNotificationAsync(CreateForkNotificationCommand xCommand) {

      ForkNotification pForkNotyification = mMapper.Map<CreateForkNotificationCommand, ForkNotification>(xCommand);
      pForkNotyification = await mForkNotyificationRepository.AddForksNotificationAsync(pForkNotyification);
      if(xCommand.Images.Count> 0) {
        new Task(() => {
          this.AddImagesForkNotyfication(xCommand.Images, pForkNotyification.IdxForkNotfication);
        }).Start();
       
      }

      return true;

    }

    private async Task AddImagesForkNotyfication(IEnumerable<ForkNotificationImage> xImages, int xIdxForkNotification) {

      foreach (var pImages in xImages) {
        pImages.IdxForkNotification = xIdxForkNotification;
        await mForkNotyificationImagesRepository.AddAsync(pImages);
      }
    }

  }
}
