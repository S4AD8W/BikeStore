using BikeStore.core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeStore.Db {
  public class BikeStoreContext : DbContext {

    public DbSet<User> Users { get; set; }

  }
}
