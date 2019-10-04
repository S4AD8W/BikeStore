using BikeStore.core.Domain;
using BikeStore.core.Domain.Notification;
using BikeStore.core.Domain.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Infrastructure.EF {
public class BikeStoreContext : DbContext{

    public DbSet<User> Users { get; set; }
    public DbSet<ForkNotification> ForksNotifications { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ForkNotificationImage> ForkNotficationImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=BikeStore;Username=postgres;Password=testt");
  }
}
