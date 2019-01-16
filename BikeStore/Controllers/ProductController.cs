using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.core.Domain;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands;
using BikeStore.ViewModels;
using BikeStore.ViewsModels;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  public class ProductController : BikeStoreControllerBaseController {

    private readonly IProductsRepository mProductsRepository;
    public int mPageSize = 3;
    public ProductController(ICommandDispatcher xCommandDispatcher, IProductsRepository xProductsRepository) 
      : base(xCommandDispatcher) {
      mProductsRepository = xProductsRepository;
    }

    public async Task<IActionResult> ListProduct(int xProductPage = 1) 
      => View(new ProductsListVM
    {
      Products = mProductsRepository.Product
                    .OrderBy(p => p.ProductID)
                    .Skip((xProductPage- 1) * mPageSize)
                    .Take(mPageSize),
      PagingInfo = new PagingInfo
      {
        CurrentPage = xProductPage,
        ItemsPerPage = mPageSize,
        TotalItems = mProductsRepository.Product.Count()
      }
    });

    public IActionResult AddProduct()
      => View();

  }
}