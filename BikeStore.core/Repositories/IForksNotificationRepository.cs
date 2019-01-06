using BikeStore.core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Repositories {
 public interface IForksNotificationRepository : IRepository {

    Task AddForksNotification(ForkNotification xForkNotification);
    Task DeleteForksNotyfication(int xId);
    Task DeleteForksNotyfication(ForkNotification xForkNotification);
    Task<IEnumerable<ForkNotification>> GetAllForksNotyfication();


  }
}
