using BikeStore.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Domain.Notification_NS.Repository {
  public interface INotificationImagesRepository :IRepository {

    IQueryable NotificationImages { get; }
    Task<NotificationImage> AddAsync(NotificationImage xNotficationImage);
    Task DeleteAsync(int xIdxNotyificationImage);
  }
}
