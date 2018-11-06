using Autofac;
using BikeStore.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BikeStore.Infrastructure.IoC.Modules {
  public class ServiceModule : Autofac.Module {                                   //Klaa odpowiadająca za konfiguracje wstrzykiwania zależność dla commend

    protected override void Load(ContainerBuilder builder) {
      var assembly = typeof(ServiceModule)
          .GetTypeInfo()                                      //pobranie typów wystepujących w inftastrukturze 
          .Assembly;

      builder.RegisterAssemblyTypes(assembly)
             .Where(x => x.IsAssignableTo<IService>())        //pobranie typów z assembly tylko tych które implementują interfejs IService
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope();                     //ustalenie czasu życia zależność 


    }
  }
}
