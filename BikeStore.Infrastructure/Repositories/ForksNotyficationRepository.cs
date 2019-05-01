using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Domain;
using BikeStore.core.Domain.Notification;
using BikeStore.core.Domain.Notification.Repository;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Infrastructure.Repositories {
  class ForksNotyficationRepository : IForkNotyficationRepository {

    public readonly BikeStoreContext mDBContext;
    public ForksNotyficationRepository(BikeStoreContext xDBContext) {
      mDBContext = xDBContext;
    }

    public IQueryable ForkNotifications => mDBContext.ForksNotifications;

    public async Task<ForkNotification> AddForksNotificationAsync(ForkNotification xForkNotification) {
      await mDBContext.ForksNotifications.AddAsync(xForkNotification);
      await mDBContext.SaveChangesAsync();
      return xForkNotification;
    }
    public Task DeleteForksNotyfication(int xId) {
      throw new NotImplementedException();
    }

    public async Task DeleteForksNotyfication(ForkNotification xForkNotification) {
      mDBContext.ForksNotifications.Remove(xForkNotification);
     await mDBContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ForkNotification>> GetAllForksNotyfication()
      => await mDBContext.ForksNotifications.ToListAsync();
  }
}
