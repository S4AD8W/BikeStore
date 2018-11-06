using Autofac;
using BikeStore.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BikeStore.Infrastructure.IoC.Modules {
  class CommandModule : Autofac.Module {
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
    }
  }
}
