using BikeStore.core.Domain;
using DatabaseEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.EF {
public class BikeStoreContext : DbContext{

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ForkNotificationEntity> ForksNotifications { get; set; }
    public DbSet<Product> Product { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=BikeStore;Username=postgres;Password=testt");
  }
}
