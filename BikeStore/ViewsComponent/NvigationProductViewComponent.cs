using BikeStore.core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Component {
  public class NvigationProductViewComponent : ViewComponent {

    
    private IProductsRepository mProductRepository;

    public NvigationProductViewComponent(IProductsRepository xProductRepository) {
      mProductRepository = xProductRepository;
    }

    public IViewComponentResult Invoke() {
      ViewBag.SelectedCategory = RouteData?.Values["category"];
      return View(mProductRepository.Products
          .Select(x => x.IdxCategory)
          .Distinct()
          .OrderBy(x => x));
    }
  }
}

