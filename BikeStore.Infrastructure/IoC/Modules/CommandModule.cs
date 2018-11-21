using Autofac;
using Microsoft.AspNetCore.Http;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Services.Emails;
using System.Reflection;

namespace BikeStore.Infrastructure.IoC.Modules{

  class CommandModule : Autofac.Module{
    //Klaa odpowiadająca za konfiguracje wstrzykiwania zależność dla commend

    protected override void Load(ContainerBuilder builder) {
      var assembly = typeof(CommandModule)
          .GetTypeInfo()
          .Assembly;                                        //pobranie typów klas dla projektu infastructury

      builder.RegisterAssemblyTypes(assembly)               //zarejestrowanie typów 
             .AsClosedTypesOf(typeof(ICommandHandler<>))    //zarejstrowanie interefejsu jaki bedzie używany dla wszystkich typów komend 
             .InstancePerLifetimeScope();                   //Ustalenie czasu życia zależonści 

      builder.RegisterType<CommandDispatcher>()             //rejestracja typu 
          .As<ICommandDispatcher>()                         //informacja jaki mabyć urzyty interfejs dla danego typu 
          .InstancePerLifetimeScope();                      //ustalenie czasu życia 

      

      builder.RegisterAssemblyTypes(assembly)
           .Where(x => x.IsAssignableTo<IEmail>())        //pobranie typów z assembly tylko tych które implementują interfejs IService
           .AsImplementedInterfaces()
           .InstancePerLifetimeScope();

      builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>()
            .SingleInstance();
    }
  }
}
 