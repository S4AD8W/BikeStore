using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Commands {
  public class CommandDispatcher : ICommandDispatcher {

    private readonly IComponentContext mContext;            //pochodzi z bibloteki autoFact 

    public CommandDispatcher(IComponentContext xContext){    //Konsturktor przyjmujący zależność wstrzkniete przez Autofact
    
      mContext = xContext;

    }

    public async Task DispatchAsync<T>(T command) where T : ICommand {
      //funkcja odpowiadajca za wywołanie kommendy 

      if (command == null) {                                //sprawdzenie czy komenda nie jest pusta 

        throw new ArgumentNullException(nameof(command),
            $"Command: '{typeof(T).Name}' can not be null.");
      }

      var handler = mContext.Resolve<ICommandHandler<T>>(); //rozpoznanie typu komendy ktura zostąła wstrzyknieta przez IoC

      await handler.HandleAsync(command);                   //wywołanie komendy 

    }
  }
}