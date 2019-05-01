using BikeStore.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Domain.Notification.Repository {
  public interface IForkNotyificationImagesRepository :IRepository {

    IQueryable ForkNotificationImages { get; }
    Task<ForkNotificationImage> AddAsync(ForkNotificationImage xForkNotficationImage);
    Task DeleteAsync(int xIdxForkNotyificationImage);
  }
}
