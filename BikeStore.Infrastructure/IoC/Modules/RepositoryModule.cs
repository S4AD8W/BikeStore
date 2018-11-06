using Autofac;
using BikeStore.core.Core.Repository;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BikeStore.Infrastructure.IoC.Modules {
  class RepositoryModule : Autofac.Module {
    protected override void Load(ContainerBuilder builder) {
      var assembly = typeof(RepositoryModule)
          .GetTypeInfo()
          .Assembly;

      builder.RegisterAssemblyTypes(assembly)
             .Where(x => x.IsAssignableTo<IRepository>())
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope();
    }
  }
}
