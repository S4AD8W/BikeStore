using BikeStore.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Domain.Notification_NS.Repository {
 public interface INotificationRepository :IRepository {

    IQueryable Notifications { get; }
    Task<Notification> AddNotificationAsync(Notification xForkNotification);
    Task DeleteNotification(int xId);
    Task DeleteNotification(Notification xForkNotification);
    Task<IEnumerable<Notification>> GetAllNotification();
  }
}
