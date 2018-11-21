using Autofac;
using BikeStore.Infrastructure.Services;
using BikeStore.Infrastructure.Services.Messages;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BikeStore.Infrastructure.IoC.Modules {

  public class ServiceModule : Autofac.Module {             //Klaa odpowiadająca za konfiguracje wstrzykiwania zależność dla commend

    protected override void Load(ContainerBuilder builder) {

      var assembly = typeof(ServiceModule)
          .GetTypeInfo()                                    //pobranie typów wystepujących w inftastrukturze 
          .Assembly;

      builder.RegisterAssemblyTypes(assembly)
             .Where(x => x.IsAssignableTo<IService>())      //pobranie typów z assembly tylko tych które implementują interfejs IService
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope();                   //ustalenie czasu życia zależność 

      builder.RegisterType<cEncrypter>()                    //zarejstrowanie typu cEncrypter jako sigleton 
              .As<IEncrypter>()
              .SingleInstance();

      builder.RegisterType<cJwtHandler>()                   //zarejstrowanie typu cJwtHandler jako sigleton 
              .As<IJwtHandler>()
              .SingleInstance();

      builder.RegisterType<cMessage>()
             .As<IMessage>()
             .InstancePerLifetimeScope(); 

    }
  }
}
