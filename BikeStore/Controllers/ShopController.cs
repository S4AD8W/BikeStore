using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BikeStore.core.Repositories;
using BikeStore.Infrastructure.Commands;
using BikeStore.ViewModels;
using BikeStore.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BikeStore.ViewModels.Shop.ProductsListVM;

namespace BikeStore.Controllers {
  public class ShopController : BikeStoreControllerBaseController {

    private readonly IProductsRepository mProductsRepository;
    private readonly IProductsCategoryRepository mProductsCategoryRepository;
    private readonly IProductImageRepository mProductImageRepository;
    private int mPageSize = 15;

    public ShopController(IMapper xMapper, ICommandDispatcher xCommandDispatcher, IProductsRepository xProductsRepository,
      IProductsCategoryRepository xProductsCategoryRepository, IProductImageRepository xProductImageRepository)
           : base(xCommandDispatcher, xMapper) {
      mProductsRepository = xProductsRepository;
      mProductsCategoryRepository = xProductsCategoryRepository;
      mProductImageRepository = xProductImageRepository;
    }


    [HttpGet]
    public async Task<IActionResult> ListOfProduct(int xIdxCategory = 0, int xProductPerPage = 1) {

      string pCurrentCategory = string.Empty;
      if (xIdxCategory != 0) pCurrentCategory = mProductsCategoryRepository.ProductsCategory.FirstOrDefault(x => x.IdxProductCategory == xIdxCategory).Name;

      return View(new ProductsListVM {
        Products = (from pProduct in mProductsRepository.Products
                            .Where(p => xIdxCategory == 0 || p.IdxCategory == xIdxCategory)
                            .OrderBy(p => p.IdxProduct)
                            .Skip((xProductPerPage - 1) * mPageSize)
                            .Take(mPageSize)
                    select (new ProductRowItem {
                      Product = pProduct,
                      ProductImage = mProductImageRepository.ProductsImages.FirstOrDefault(x => x.IdxProduct == pProduct.IdxProduct)
                    })).ToList(),

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
    public async Task<IActionResult> DetailProduct(int xIdxProduct) {

      DetailProductVM pDetailProduct = new DetailProductVM {
        Product = await mProductsRepository.GetAsync(xIdxProduct),
        ProductImages = await mProductImageRepository.GetAllImageForIdxProduct(xIdxProduct)
      };

      return View(pDetailProduct);
    }


  }
}