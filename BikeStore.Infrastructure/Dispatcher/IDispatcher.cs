using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Query;
using BikeStore.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BikeStore.Infrastructure.Dispatcher {
 public interface IDispatcher {
    Task<CommandResult> SendAsync<TCommand>(TCommand xCommand) where TCommand : ICommand;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> xQuery);
  }
}
