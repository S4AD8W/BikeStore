using Autofac;
using BikeStore.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BikeStore.Infrastructure.IoC.Modules {
 public class QueryModule : Autofac.Module {

    protected override void Load(ContainerBuilder xBuilder) {
      var assembly = typeof(QueryModule)
          .GetTypeInfo()
          .Assembly;                                        //pobranie typów klas dla projektu infastructury

      xBuilder.RegisterAssemblyTypes(assembly)               //zarejestrowanie typów 
             .AsClosedTypesOf(typeof(IQueryHandler<,>))    //zarejstrowanie interefejsu jaki bedzie używany dla wszystkich typów komend 
             .InstancePerLifetimeScope();                   //Ustalenie czasu życia zależonści 

      xBuilder.RegisterType<QueryDispatcher>()             //rejestracja typu 
          .As<IQueryDispatcher>()                         //informacja jaki mabyć urzyty interfejs dla danego typu 
          .InstancePerLifetimeScope();                      //ustalenie czasu życia 
    }
  }
}
