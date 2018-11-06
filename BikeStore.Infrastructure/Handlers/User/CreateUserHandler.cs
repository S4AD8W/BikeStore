using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.User;
using BikeStore.Infrastructure.Services;
using System;

using System.Threading.Tasks;
namespace BikeStore.Infrastructure.Handlers.User {
  class CreateUserHandler : ICommandHandler<CreateUser> {
    private readonly IUserService mUserService;

    public CreateUserHandler(IUserService UserService) {

      mUserService = UserService;
    }

    public async Task HandleAsync(CreateUser xCommand) {

      await mUserService.RegisterAsync(new Guid(), xCommand.Email, xCommand.Username, xCommand.Password,
        xCommand.Name, xCommand.Surname);                   //Dodanie użytkownika 
    }


  }

}
