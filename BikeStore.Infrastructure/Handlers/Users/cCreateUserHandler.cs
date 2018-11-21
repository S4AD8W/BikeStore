using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Users;
using BikeStore.Infrastructure.Services;
using BikeStore.Infrastructure.Services.Emails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BikeStore.Infrastructure.Handlers.Users {

  class cCreateUserHandler : ICommandHandler<CreateUser> {

    private readonly IUserService mUserService;
    private readonly IEmailService mEmailService;

    public cCreateUserHandler(IUserService UserService, IEmailService xEmailService) {

      mUserService = UserService;
      mEmailService = xEmailService; 
    }

    public async Task HandleAsync(CreateUser xCommand) {
      bool pIsResult;
      Guid pUserID;

      pUserID = new Guid();
      pIsResult = await mUserService.RegisterAsync(pUserID,xCommand.Email, xCommand.Name, xCommand.Password, "User");                   //Dodanie użytkownika 

      if (pIsResult) {
       new Thread(() => {

        Thread.CurrentThread.IsBackground = true;
          mEmailService.SendUserAccountConfirmation(xCommand.Email, pUserID);
        }).Start();
      }

    }


  }

}