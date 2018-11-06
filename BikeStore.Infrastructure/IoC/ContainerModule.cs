using Autofac;
using BikeStore.Infrastructure.IoC.Modules;
using BikeStore.Infrastructure.Mappers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.IoC {
  public class ContainerModule : Autofac.Module {

    private readonly IConfiguration _configuration;

    public ContainerModule(IConfiguration configuration) {
      _configuration = configuration;

    }

    protected override void Load(ContainerBuilder builder) {
      //fukcja odpowiadająca z rejestracje modułów w aplikacji. po staremu było w startup.cs 

      builder.RegisterInstance(AutoMapperConfig.Initialize())
        .SingleInstance();
      builder.RegisterModule<RepositoryModule>();
      builder.RegisterModule<CommandModule>();
      builder.RegisterModule<ServiceModule>();
    }

  }

}