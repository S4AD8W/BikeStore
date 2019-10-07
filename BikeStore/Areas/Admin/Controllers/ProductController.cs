using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Areas.Admin.ViewModel.Product;
using BikeStore.Controllers;
using BikeStore.core.Domain.Product_NS;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Product;
using BikeStore.Infrastructure.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Areas.Admin.Controllers {
  [Area("Admin")]
  public class ProductController : BikeStoreControllerBaseController {

    private readonly BikeStoreContext mDB;

    public ProductController(BikeStoreContext xDB, IMapper xMapper, ICommandDispatcher xCommandDispatcher)
           : base(xCommandDispatcher, xMapper) {
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

      AddProductCommand pCommand;

      pCommand = mMapper.Map<ProductVM, AddProductCommand>(xVM);

      if (xFiles != null) {                                 //sprawdzenie czy lista plików niejest pusta i odczytanie zawatość 
        pCommand.Images = new List<ProductImage>();
        using (var memoryStream = new MemoryStream()) {
          foreach (var pFile in xFiles) {
            await pFile.CopyToAsync(memoryStream);
            pCommand.Images.Add(new ProductImage {
              Content = memoryStream.ToArray(),
              Name = pFile.FileName,
              Size = pFile.Length
            });
          }
        }
      }

      await DispatchAsync(pCommand);

      return RedirectToAction("AddProduct");

    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(string Name) {

      ProductCategory pProductCategory = new ProductCategory(Name);
      mDB.ProductCategory.Add(pProductCategory);
      await mDB.SaveChangesAsync();

      return RedirectToAction("AddProduct");
      //TODO:Dorobić ajaxem sprawdzanie czy kategoria produktu już istnieje 
    }

  }
}