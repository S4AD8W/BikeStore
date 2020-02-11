using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeStore.core.Domain.OrderNS;
using BikeStore.core.Repositories;

namespace BikeStore.core.Domain.OrderNS.Repository {
public interface IOrderRepository : IRepository{

    IQueryable<Order> Orders { get; }
    Task<int> AddOrderAsync(Order xOrder);
    Task<double> GetPriceOrder(int xIdxOrder);
    Task<OrderNS.Order> GetOrder(int xIdxOrder);
    Task<IEnumerable<Order>> GetOrders(int xIdxUser);
    
  }
}
