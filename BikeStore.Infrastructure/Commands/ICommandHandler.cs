
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Commands {

  public interface ICommandHandler<T> where T : ICommand{
    //interfejs odpowiadający za uruchomienie komendy 

    Task HandleAsync(T command);

  }
}
