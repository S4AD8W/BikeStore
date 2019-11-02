using BikeStore.core.Domain.Notification_NS;
using BikeStore.core.Domain.Notification_NS.Repository;
using BikeStore.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Notification_NS.Repository {
  public class NotificationImagesRepository : INotificationImagesRepository {

    private readonly BikeStoreContext mDB;
    public NotificationImagesRepository(BikeStoreContext xDB) {
      mDB = xDB;
    }

    public IQueryable NotificationImages => mDB.NotificationImages;

    public async Task<NotificationImage> AddAsync(NotificationImage xForkNotficationImage) {

      await mDB.NotificationImages.AddAsync(xForkNotficationImage);
      await mDB.SaveChangesAsync();

      return xForkNotficationImage;

    }

    public async Task DeleteAsync(int xIdxNotificationImage) {

      NotificationImage pForkNotficationImage;

      pForkNotficationImage = mDB.NotificationImages.SingleOrDefault(x => x.IdxNotoificationImage == xIdxNotificationImage);
      if(pForkNotficationImage != null) {
        mDB.NotificationImages.Remove(pForkNotficationImage);
       await mDB.SaveChangesAsync();
      }
    
    }
  }
}
