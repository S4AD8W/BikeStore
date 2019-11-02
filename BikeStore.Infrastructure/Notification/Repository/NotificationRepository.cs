using BikeStore.core.Domain.Notification_NS;
using BikeStore.core.Domain.Notification_NS.Repository;
using BikeStore.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Notification_NS.Repository {
  public class NotificationRepository : INotificationRepository {

    private readonly BikeStoreContext mDB;

    public NotificationRepository(BikeStoreContext xDB) {
      mDB = xDB;
    }
    public IQueryable Notifications => mDB.Notifications;

    public async Task<Notification> AddNotificationAsync(Notification xForkNotification) {

      await mDB.Notifications.AddAsync(xForkNotification);
      await mDB.SaveChangesAsync();

      return xForkNotification;

    }

    public Task DeleteNotification(int xId) {
      throw new NotImplementedException();
    }

    public Task DeleteNotification(Notification xNotification) {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Notification>> GetAllNotification() {
      throw new NotImplementedException();
    }
  }
}
