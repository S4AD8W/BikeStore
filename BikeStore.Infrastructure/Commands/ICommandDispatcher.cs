using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Commands {
  public interface ICommandDispatcher {
    // Interfejs odpowiadający za rozdysponwyaniem komend 

    Task DispatchAsync<T>(T command) where T : ICommand;

  }
}
