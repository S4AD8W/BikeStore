using BikeStore.core.Domain.Notification;
using BikeStore.core.Domain.Notification.Repository;
using BikeStore.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Notification.Repository {
  public class ForkNotficationImageImagesRepository : IForkNotyificationImagesRepository {

    private readonly BikeStoreContext mDB;
    public ForkNotficationImageImagesRepository(BikeStoreContext xDB) {
      mDB = xDB;
    }

    public IQueryable ForkNotificationImages => mDB.ForkNotficationImages;

    public async Task<ForkNotificationImage> AddAsync(ForkNotificationImage xForkNotficationImage) {

      await mDB.ForkNotficationImages.AddAsync(xForkNotficationImage);
      await mDB.SaveChangesAsync();

      return xForkNotficationImage;

    }

    public async Task DeleteAsync(int xIdxForkNotyificationImage) {

      ForkNotificationImage pForkNotficationImage;

      pForkNotficationImage = mDB.ForkNotficationImages.SingleOrDefault(x => x.IdxForkNotoificationImage == xIdxForkNotyificationImage);
      if(pForkNotficationImage != null) {
        mDB.ForkNotficationImages.Remove(pForkNotficationImage);
       await mDB.SaveChangesAsync();
      }
    
    }
  }
}
