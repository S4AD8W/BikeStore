using BikeStore.Infrastructure.Notification_NS.Commands;
using BikeStore.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Notification_NS.Services {
  public interface INotificationService : IService {
    Task<(bool IsSucces, Guid ForkNotification_Guid)> AddNotificationAsync(CreateNotificationCommand xCommand);
  }
}
