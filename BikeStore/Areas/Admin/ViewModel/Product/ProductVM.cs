using BikeStore.core.Domain.Notification;
using BikeStore.Infrastructure.EF;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Areas.Admin.ViewModel.Product {
  public class ProductVM {

    private readonly BikeStoreContext mDb;

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int IdxCategory { get; set; }
    public int IdxProduct { get; set; }
    public int Quantity { get; set; }

    public List<SelectListItem> ProductCategories { get; set; }

    public ProductVM() {

    }
    public ProductVM(BikeStoreContext xDb) {
      mDb = xDb;
    }
    
    public async Task Initialize() {

      this.ProductCategories = await mDb.ProductCategory.Select(x => new SelectListItem {
        Text = x.Name,
        Value = x.IdxProductCategory.ToString()
      }).ToListAsync();
    }

  }
}
