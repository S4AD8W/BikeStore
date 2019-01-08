using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.core.Domain;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Controllers {
  public class ProductController : BikeStoreControllerBaseController {

    private readonly IProductsRepository mProductsRepository;
    public int mPageSize = 3;
    protected ProductController(ICommandDispatcher xCommandDispatcher, IProductsRepository xProductsRepository) 
      : base(xCommandDispatcher) {
      mProductsRepository = xProductsRepository;
    }

    public async Task<IActionResult> ListProduct(int xPageSize = 1) {
      IEnumerable<Product> pProducts = await mProductsRepository.GetAllProductAsync();

      return View(pProducts.OrderBy(p => p.ProductID)
                            .Skip((xPageSize-1)*xPageSize)
                            .Take(xPageSize));
    }

    public IActionResult AddProduct()
      => View();

  }
}