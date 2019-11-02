using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeStore.core.Domain.Notification_NS {

  public enum NotificationStatusEnum {
    New = 1,
    AtWork =2,
    Finish =3,
  }

  public enum NotificationTypeEnum {
    Fork = 1,
    Bike= 2,
  }

  public class Notification {
    [Key]
    public int IdxNotification { get; private set; }
    public Guid Guid { get; private set; }
    public DateTime CreateAt { get; private set; }
    public DateTime UpdateAt { get; private set; }
    public NotificationStatusEnum NotificationStatus { get; private set; }
    public string Dscr { get; private set; }
    public Guid UserUuId { get; private set; }
    public int IdxUser { get; private set; }
    public NotificationTypeEnum NotificationType { get; private set; }
    public string Model { get; private set; }
    public string Brand { get; private set; }

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
