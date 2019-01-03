using BikeStore.core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.EF {
public class BikesStoreContext : DbContext{

    public DbSet<User> Users { get; set; }
    public DbSet<ForkNotification> ForksNotifications { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=BikeStore;Username=postgres;Password=testt");
  }
}
