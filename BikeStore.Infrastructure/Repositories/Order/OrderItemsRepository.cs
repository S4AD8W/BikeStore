using BikeStore.core.Domain.OrderNS;
using BikeStore.core.Domain.OrderNS.Repository;
using BikeStore.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Repositories.Order {
  public class OrderItemsRepository : IOrderItemsRepository {

    private readonly BikeStoreContext mDbContext;
    public OrderItemsRepository(BikeStoreContext xDbContext) {
      mDbContext = xDbContext;
    }
    public IQueryable<OrderItem> OrderItems => mDbContext.OrderItems;

    public async Task AddOrderItemsAsync(IEnumerable<OrderItem> xOrderItems) {
      await mDbContext.OrderItems.AddRangeAsync(xOrderItems);
      await mDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItems(int xIdxOrder) {
      return await mDbContext.OrderItems.Where(pOrderItem => pOrderItem.IdxOrder == xIdxOrder).ToListAsync();
    }

    public Task<bool> UpdateIdxTradeDocItemAsync(int xIdxOrderItem, int xIdxTradeDocItem) {
      throw new NotImplementedException();
    }
  }
}
