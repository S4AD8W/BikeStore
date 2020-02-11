using BikeStore.core.Domain;
using BikeStore.core.Domain.Notification_NS;
using BikeStore.core.Domain.OrderNS;
using BikeStore.core.Domain.Product_NS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.EF {
  public class BikeStoreContext : DbContext {

    public DbSet<User> Users { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<NotificationImage> NotificationImages { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=BikeStore;Username=postgres;Password=test");

    protected override void OnModelCreating(ModelBuilder xModelBuilder) {

      xModelBuilder.HasDefaultSchema("public");             //ustwienie domyślnego schematu bazy danych

      base.OnModelCreating(xModelBuilder);

      //przy tworzeniu tabeli musimy skonwertować nazwę poszczególnych obiektw do małej litery

      foreach (var pEntity in xModelBuilder.Model.GetEntityTypes()) {
        pEntity.Relational().TableName = pEntity.Relational().TableName.ToLower();

        //kolumny
        foreach (var pProperty in pEntity.GetProperties()) {
          pProperty.Relational().ColumnName = pProperty.Name.ToLower();
        }

        //klucz główny
        foreach (var key in pEntity.GetKeys()) {
          key.Relational().Name = key.Relational().Name.ToLower();
        }

        //klucze obce
        foreach (var pKey in pEntity.GetForeignKeys()) {
          pKey.Relational().Name = pKey.Relational().Name.ToLower();
        }

        //indeks w tabeli
        foreach (var pIndex in pEntity.GetIndexes()) {
          pIndex.Relational().Name = pIndex.Relational().Name.ToLower();
        }

      }

    }



  }
}
