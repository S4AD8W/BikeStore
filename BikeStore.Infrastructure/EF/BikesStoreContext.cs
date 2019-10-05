using BikeStore.core.Domain;
using BikeStore.core.Domain.Notification;
using BikeStore.core.Domain.Product_NS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.EF {
  public class BikeStoreContext : DbContext {

    public DbSet<User> Users { get; set; }
    public DbSet<ForkNotification> ForksNotifications { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ForkNotificationImage> ForkNotficationImages { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=BikeStore;Username=postgres;Password=testt");

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
