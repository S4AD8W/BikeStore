using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Query {
  public interface IQueryDispatcher {

    Task<TResult> QueryAsync<TResult>(IQuery<TResult> xQuery);
  }
}
