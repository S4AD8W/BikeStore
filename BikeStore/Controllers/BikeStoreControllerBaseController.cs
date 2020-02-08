using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Dispatcher;
using BikeStore.Infrastructure.Query;
using BikeStore.Infrastructure.Types;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  public class BikeStoreControllerBaseController : Controller {

    private readonly IDispatcher mDispatcher;
    public readonly IMapper mMapper;
    public CommandResult CommandResult { get; private set; }

    protected BikeStoreControllerBaseController(IDispatcher xCommandDispatcher, IMapper xMapper) {

      mDispatcher = xCommandDispatcher;
      mMapper = xMapper;
    }

    protected async Task<CommandResult> SendAsync<T>(T xCommand) where T : ICommand {

      this.CommandResult = await mDispatcher.SendAsync(xCommand);

      return this.CommandResult;

    }

    protected async Task<TResult> QueryAsync<TResult>(IQuery<TResult> xQuery) {

      return await mDispatcher.QueryAsync(xQuery);

    }

  }
}

