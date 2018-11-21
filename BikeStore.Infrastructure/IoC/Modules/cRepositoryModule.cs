using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BikeStore.core.Repositories;

namespace BikeStore.Infrastructure.IoC.Modules {
  class cRepositoryModule : Autofac.Module {
    //Klasa zawierająca moduł z repozytoriami żeby wstrzyknąć zależność dla repozytoriów 

    protected override void Load(ContainerBuilder builder) {
      var assembly = typeof(cRepositoryModule)              
          .GetTypeInfo()                                    //pobranie określonych typów z assembly 
          .Assembly;

      builder.RegisterAssemblyTypes(assembly)
             .Where(x => x.IsAssignableTo<IRepository>())   //wybranie z asebly typów oznaczonych interfejsem 
             .AsImplementedInterfaces()                     
             .InstancePerLifetimeScope();                   //ustawienie czasu życia zależność 
    }
  }
}
