using BikeStore.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.core.Domain.OrderNS.Repository {
  public interface IOrderItemsRepository : IRepository {

    IQueryable<OrderItem> OrderItems { get; }
    Task AddOrderItemsAsync(IEnumerable<OrderItem> xOrderItems);
    Task<IEnumerable<OrderItem>> GetOrderItems(int xIdxOrder);
    Task<bool> UpdateIdxTradeDocItemAsync(int xIdxOrderItem, int xIdxTradeDocItem);

  }

}

