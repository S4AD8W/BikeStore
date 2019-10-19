using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.Areas.Admin.ViewModel.Product;
using BikeStore.Controllers;
using BikeStore.core.Domain.Product_NS;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands;
using BikeStore.Infrastructure.Commands.Product;
using BikeStore.Infrastructure.EF;
using BikeStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Areas.Admin.Controllers {
  [Area("Admin")]
  public class ProductController : BikeStoreControllerBaseController {

    private readonly BikeStoreContext mDB;
    private readonly IProductsRepository mProductsRepository;
    private readonly IProductsCategoryRepository mProductsCategoryRepository;
    private readonly IProductImageRepository mProductImageRepository;
    private int mPageSize = 15;

    public ProductController(BikeStoreContext xDB, IMapper xMapper, ICommandDispatcher xCommandDispatcher, IProductsRepository xProductsRepository,
      IProductsCategoryRepository xProductsCategoryRepository, IProductImageRepository xProductImageRepository)
           : base(xCommandDispatcher, xMapper) {
      mDB = xDB;
      mProductsRepository = xProductsRepository;
      mProductsCategoryRepository = xProductsCategoryRepository;
      mProductImageRepository = xProductImageRepository;
    }

    [HttpGet]
    public async Task<IActionResult> AddProduct() {

      ProductVM pVM = new ProductVM(mDB);

      await pVM.Initialize();

      return View(pVM);

    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductVM xVM, ICollection<IFormFile> xFiles) {

      AddProductCommand pCommand;
      //TODO:Dodać walidację plików po roszerzeniu 
      pCommand = mMapper.Map<ProductVM, AddProductCommand>(xVM);

      if (xFiles != null) {                                 //sprawdzenie czy lista plików niejest pusta i odczytanie zawatość 
        pCommand.Images = new List<ProductImage>();
        foreach (var pFile in xFiles) {
          using (var memoryStream = new MemoryStream()) {
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


    [HttpGet]
    public async Task<IActionResult> List(int xIdxCategory = 0, int xProductPerPage = 1) {

      string pCurrentCategory = string.Empty;
      if (xIdxCategory != 0) pCurrentCategory = mProductsCategoryRepository.ProductsCategory.FirstOrDefault(x => x.IdxProductCategory == xIdxCategory).Name;

      return View(new BikeStore.Areas.Admin.ViewModel.Product.ProductListVM {
        Products = mProductsRepository.Products
                            .Where(p => xIdxCategory == 0 || p.IdxCategory == xIdxCategory)
                            .OrderBy(p => p.IdxProduct)
                            .Skip((xProductPerPage - 1) * mPageSize)
                            .Take(mPageSize),
        PagingInfo = new PagingInfo {
          CurrentPage = xProductPerPage,
          ItemsPerPage = mPageSize,
          TotalItems = xIdxCategory == 0 ?
                                mProductsRepository.Products.Count() :
                                mProductsRepository.Products.Where(e =>
                                    e.IdxCategory == xIdxCategory).Count()
        },
        CurrentCategory = pCurrentCategory,
        ProductCategories = await mProductsCategoryRepository.ProductsCategory.ToListAsync()

      });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int xIdxProduct) {

      Product pProduct = await mProductsRepository.GetAsync(xIdxProduct);
      IEnumerable<ProductImage> pProductImages = await mProductImageRepository.GetAllImageForIdxProduct(pProduct.IdxProduct);

      EditProductVM pVM = new EditProductVM(pProduct, pProductImages, await mProductsCategoryRepository.ProductsCategory.ToListAsync());
     
      return View(pVM);

    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditProductVM xVM, ICollection<IFormFile> xFiles) {
      EditProductCommand pCommand;

      pCommand = mMapper.Map<EditProductVM, EditProductCommand>(xVM);

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

      return RedirectToAction("List");

    }

    [HttpGet]
    public async Task<IActionResult> DelePhotos(int xIdxPhoto, int xIdxProduct) {
      await mProductImageRepository.DeleteAsync(xIdxPhoto);
      return RedirectToAction("Edit", new { xIdxProduct = xIdxProduct });

    }

    [HttpGet]
    public async Task<IActionResult> Delete(int xIdxProduct) {
      await mProductsRepository.DeleteProductAsync(xIdxProduct);

      return RedirectToAction("List");
    }
  }
}