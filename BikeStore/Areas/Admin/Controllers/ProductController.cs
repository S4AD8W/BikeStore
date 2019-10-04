using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Areas.Admin.ViewModel.Product;
using BikeStore.Infrastructure.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Areas.Admin.Controllers {
  [Area("Admin")]
  public class ProductController : Controller {

    private readonly BikeStoreContext mDB;

    public ProductController(BikeStoreContext xDB) {
      mDB = xDB;
    }
    [HttpGet]
    public async Task<IActionResult> AddProduct() {

      ProductVM pVM = new ProductVM(mDB);

      await pVM.Initialize();

      return View(pVM);

    }

    [HttpPost]
    public async Task<IActionResult> AddProduct( ProductVM xVM, ICollection<IFormFile> xFiles) {

      return View();
    }
  }
}