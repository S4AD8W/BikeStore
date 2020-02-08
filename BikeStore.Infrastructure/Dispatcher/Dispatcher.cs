using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Query;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Dispatcher {
  public class Dispatcher: IDispatcher {

    private readonly ICommandDispatcher mCommandDispatcher;
  private readonly IQueryDispatcher mQueryDispatcher;

  public Dispatcher(ICommandDispatcher xCommandDispatcher, IQueryDispatcher xQueryDispatcher) {
    mCommandDispatcher = xCommandDispatcher;
    mQueryDispatcher = xQueryDispatcher;
  }


  public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> xQuery)
    => await mQueryDispatcher.QueryAsync<TResult>(xQuery);
    

    public async Task<CommandResult> SendAsync<TCommand>(TCommand xCommand) where TCommand : ICommand
    => await mCommandDispatcher.DispatchAsync(xCommand);


}
}