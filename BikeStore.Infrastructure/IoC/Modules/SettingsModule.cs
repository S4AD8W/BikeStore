using Autofac;
using BikeStore.Infrastructure.Extensions;
using BikeStore.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.IoC.Modules {
  class SettingsModule : Autofac.Module {
    private readonly IConfiguration mConfiguration;

    public SettingsModule(IConfiguration xConfiguration) {
      mConfiguration = xConfiguration;
    }

    protected override void Load(ContainerBuilder builder) {
      builder.RegisterInstance(mConfiguration.GetSettings<SmtpClientSettings>())
             .SingleInstance();

    }
  }
}