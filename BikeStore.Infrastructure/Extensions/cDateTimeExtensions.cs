using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.Extensions {

  public static class DateTimeExtensions {

    public static long ToTimestamp(this DateTime xDateTime) {
      var pEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
      var pTime = xDateTime.Subtract(new TimeSpan(pEpoch.Ticks));

      return pTime.Ticks / 10000;

    }

  }
}
