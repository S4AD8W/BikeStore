using AutoMapper;
using BikeStore.core.Domain.Notification_NS;
using BikeStore.core.Domain.Notification_NS.Repository;
using BikeStore.Infrastructure.Notification_NS.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Notification_NS.Services {
  public class NotificationService : INotificationService {

    private readonly INotificationRepository mNotificationRepository;
    private readonly INotificationImagesRepository mNotificationImagesRepository;
    private readonly IMapper mMapper;

    public NotificationService(INotificationRepository xForkNotificationRepository, INotificationImagesRepository xNotificationImagesRepository,
      IMapper xMapper) {
      mNotificationImagesRepository = xNotificationImagesRepository;
      mNotificationRepository = xForkNotificationRepository;
      mMapper = xMapper;
    }

    public async Task<(bool IsSucces, Guid ForkNotification_Guid)> AddNotificationAsync(CreateNotificationCommand xCommand) {

      Notification pNotification = mMapper.Map<CreateNotificationCommand, Notification>(xCommand);
      pNotification = await mNotificationRepository.AddNotificationAsync(pNotification);
      if(xCommand.Images.Count> 0) {
        new Task(() => {
          this.AddImagesNotification(xCommand.Images, pNotification.IdxNotification);
        }).Start();
       
      }

      return (IsSucces:true, ForkNotification_Guid: pNotification.Guid);

    }

    private async Task AddImagesNotification(IEnumerable<NotificationImage> xImages, int xIdxForkNotification) {

      foreach (var pImages in xImages) {
        pImages.IdxNotification = xIdxForkNotification;
        await mNotificationImagesRepository.AddAsync(pImages);
      }
    }

  }
}
