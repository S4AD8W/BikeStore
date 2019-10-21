using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using BikeStore.Infrastructure.IoC;
using BikeStore.Infrastructure.EF;
using BikeStore.Infrastructure;
using BikeStore.core.Domain;
using BikeStore.Infrastructure.Services;

namespace BikeStore {
  public class Startup {

    public IConfigurationRoot Configuration { get; }
    public IContainer ApplicationContainer { get; private set; }

    public Startup(IHostingEnvironment env) {

      var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
      Configuration = builder.Build();
    }



    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection xServices) {

      xServices.AddSingleton<Languages>(new Languages(new HttpContextAccessor()));

      xServices.AddAuthentication(o => {
        o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      }).AddCookie(options => {
        options.AccessDeniedPath = new PathString("/Account/Login");
        options.LoginPath = new PathString("/Account/Login");
      });

      xServices.AddAuthorization(options => {
        options.AddPolicy("RequiresAdmin", policy => policy.RequireClaim("HasAdminRights"));
      });

      xServices.AddMvc().AddJsonOptions(options => {
        // options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
      });

      xServices.AddScoped<Cart>(x => SessionCart.GetCart(x));

      xServices.AddMemoryCache();
      xServices.AddSession();

      xServices.AddEntityFrameworkNpgsql()
                .AddDbContext<BikeStoreContext>()
                .BuildServiceProvider();

      var builder = new ContainerBuilder();
      builder.Populate(xServices);
      builder.RegisterModule(new ContainerModule(Configuration));
      ApplicationContainer = builder.Build();

      return new AutofacServiceProvider(ApplicationContainer);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {

      if (env.IsDevelopment()) {
        app.UseBrowserLink();
        app.UseDeveloperExceptionPage();
      } else {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseAuthentication();
      app.UseStaticFiles();
      app.UseSession();
      app.UseMvc(routes => {
        routes.MapAreaRoute(
            name: "Admin",
            areaName: "Admin",
            template: "Admin/{controller=Home}/{action=Index}/{id?}");

        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");

      });


    }
  }
}

