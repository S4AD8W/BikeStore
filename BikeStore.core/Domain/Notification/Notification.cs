using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.core.Domain.Notification {

  public enum NotificationStatusEnum {
    New = 1,
    AtWork =2,
    Finish =3,
  }

 public class Notification {
    public Guid Guid { get; private set; }
    public DateTime CreateAt { get; private set; }
    public DateTime UpdateAt { get; private set; }
    public NotificationStatusEnum NotificationStatus { get; private set; }

    public Notification() {

      this.CreateAt = DateTime.UtcNow;
      this.Guid = Guid.NewGuid();
      this.NotificationStatus = NotificationStatusEnum.New;
    }

    public void ChangeNotificationStatus(NotificationStatusEnum xStatus) {

      this.NotificationStatus = xStatus;

    }
  }
}
