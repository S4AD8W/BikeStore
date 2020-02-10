using BikeStore.core.Domain.OrderNS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Domain.OrderNS;
using BikeStore.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Infrastructure.Repositories.Order {
  public class OrderRepository : IOrderRepository {

    private readonly BikeStoreContext mDbContext;

    public OrderRepository(BikeStoreContext xDbContext) {
      mDbContext = xDbContext;
    }
    public IQueryable<core.Domain.OrderNS.Order> Orders => mDbContext.Orders;

    public async Task<int> AddOrderAsync(core.Domain.OrderNS.Order xOrder) {
      try {

        await mDbContext.Orders.AddAsync(xOrder);
        await mDbContext.SaveChangesAsync();

      } catch (Exception EX) {

        throw;
      }

      return xOrder.IdxOrder;

    }

    public  Task<core.Domain.OrderNS.Order> GetOrder(int xIdxOrder) {
      
      core.Domain.OrderNS.Order pOrder;

      pOrder = mDbContext.Orders.FirstOrDefault(Order => Order.IdxOrder == xIdxOrder);

      return Task.FromResult(pOrder);
    }

    public async Task<IEnumerable<core.Domain.OrderNS.Order>> GetOrders(int xIdxUser) {
      return await mDbContext.Orders.Where(pOrder => pOrder.IdxUser == xIdxUser).ToListAsync();
    }

    public  Task<double> GetPriceOrder(int xIdxOrder) {
      double pPrice;

      pPrice = mDbContext.OrderItems.Where(x => x.IdxOrder == xIdxOrder).Sum(p => p.UnitPrice);

      return Task.FromResult(pPrice);
    }

    
  }
}
