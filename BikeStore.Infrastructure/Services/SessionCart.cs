using BikeStore.core.Domain;
using BikeStore.core.Domain.Product_NS;
using BikeStore.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace BikeStore.Infrastructure.Services {
  public class SessionCart : Cart {

    public static Cart GetCart(IServiceProvider xService) {

      ISession pSession = xService.GetRequiredService<IHttpContextAccessor>()?
                    .HttpContext.Session;

      SessionCart pCart = pSession?.GetJson<SessionCart>("Cart")
        ?? new SessionCart();
      pCart.Session = pSession;
      return pCart;
    }

    [JsonIgnore]
    public ISession Session { get; set; }

    public override void AddItem(Product xProduct, int xQuantity) {
      base.AddItem(xProduct, xQuantity);
      Session.SetJson("Cart", this);
    }

    public override void RemoveLine(Product xProduct) {
      base.RemoveLine(xProduct);
      Session.SetJson("Cart", this);
    }

    public override void Clear() {
      base.Clear();
      Session.SetJson("Cart", this);
    }

  }
}
