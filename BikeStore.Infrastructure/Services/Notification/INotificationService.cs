using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Services.Notification {
  interface INotificationService : IService {

    Task AddForksNotification(ForkNotification xForkNotification);
  }
}
