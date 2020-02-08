using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Query {
  public class QueryDispatcher : IQueryDispatcher {

    private readonly IComponentContext mContext;

    public QueryDispatcher(IComponentContext xContext) {
      mContext = xContext;

    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> xQuery) {

      var pHandlerType = typeof(IQueryHandler<,>)
            .MakeGenericType(xQuery.GetType(), typeof(TResult));

      dynamic handler = mContext.Resolve(pHandlerType);

      return await handler.HandleAsync((dynamic)xQuery);

    }
  }
}
