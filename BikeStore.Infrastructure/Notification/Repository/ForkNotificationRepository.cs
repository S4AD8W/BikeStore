using BikeStore.core.Domain.Notification;
using BikeStore.core.Domain.Notification.Repository;
using BikeStore.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Notification.Repository {
  public class ForkNotificationRepository : IForkNotyficationRepository {

    private readonly BikeStoreContext mDB;

    public ForkNotificationRepository(BikeStoreContext xDB) {
      mDB = xDB;
    }
    public IQueryable ForkNotifications => throw new NotImplementedException();

    public async Task<ForkNotification> AddForksNotificationAsync(ForkNotification xForkNotification) {

      await mDB.ForksNotifications.AddAsync(xForkNotification);
      await mDB.SaveChangesAsync();

      return xForkNotification;

    }

    public Task DeleteForksNotyfication(int xId) {
      throw new NotImplementedException();
    }

    public Task DeleteForksNotyfication(ForkNotification xForkNotification) {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<ForkNotification>> GetAllForksNotyfication() {
      throw new NotImplementedException();
    }
  }
}
