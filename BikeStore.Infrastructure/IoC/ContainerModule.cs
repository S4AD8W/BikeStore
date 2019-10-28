using Autofac;
using Microsoft.Extensions.Configuration;
using BikeStore.Infrastructure.IoC.Modules;
using BikeStore.Infrastructure.Mappers;


namespace BikeStore.Infrastructure.IoC {

  public class ContainerModule : Autofac.Module {
    //Klasa zawierająca moduły które rejestruje w aplikacji

    private readonly IConfiguration mConfiguration;         //interfejs konfiguracji aplikacji 
    
    public ContainerModule(IConfiguration configuration) {
      mConfiguration = configuration;

    }

    protected override void Load(ContainerBuilder builder) {
      //fukcja odpowiadająca z rejestracje modułów w aplikacji. po staremu było w startup.cs 

      builder.RegisterInstance(cAutoMapperConfig.Initialize())
        .SingleInstance();
      builder.RegisterModule<cRepositoryModule>();
      builder.RegisterModule<CommandModule>();
      builder.RegisterModule<ServiceModule>();
      builder.RegisterModule<SqlModule>();
      builder.RegisterModule(new SettingsModule(mConfiguration));
    }

  }

}
