using BikeStore.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Domain.Notification.Repository {
 public interface IForkNotyficationRepository :IRepository {

    IQueryable ForkNotifications { get; }
    Task<ForkNotification> AddForksNotificationAsync(ForkNotification xForkNotification);
    Task DeleteForksNotyfication(int xId);
    Task DeleteForksNotyfication(ForkNotification xForkNotification);
    Task<IEnumerable<ForkNotification>> GetAllForksNotyfication();
  }
}
