using BikeStore.Infrastructure.Notification.Commands;
using BikeStore.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Notification.Services {
  public interface IForkNotificationService : IService {
    Task<bool> AddForkNotificationAsync(CreateForkNotificationCommand xCommand);
  }
}
