using Autofac;
using Microsoft.Extensions.Configuration;
using BikeStore.Infrastructure.Extensions;
using BikeStore.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.IoC.Modules {
  class SettingsModule : Autofac.Module {
    //Klasa zawierająca moduł z ustawień żeby wstrzyknąć zależność dla ustawień aplikacji  

    private readonly IConfiguration mConfiguration;         //interfejs konfiguracji


    public SettingsModule(IConfiguration xConfiguration) {
      mConfiguration = xConfiguration;
    }

    protected override void Load(ContainerBuilder builder) {
      builder.RegisterInstance(mConfiguration.GetSettings<SmtpClientSettings>()) //zarejestrowanie typu konfiguracji 
             .SingleInstance();                             //określenie czasu życia 

      builder.RegisterInstance(mConfiguration.GetSettings<cJwtSettings>())
              .SingleInstance();
      builder.RegisterInstance(mConfiguration.GetSettings<PayUSettings>())
              .SingleInstance();
    }

  }
}
