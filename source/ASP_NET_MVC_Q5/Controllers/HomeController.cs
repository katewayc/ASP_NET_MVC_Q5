using Newtonsoft.Json;
using ASP_NET_MVC_Q5.Models;
using ASP_NET_MVC_Q5.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace ASP_NET_MVC_Q5.Controllers
{
    public class HomeController : Controller
    {
        private int pageSize = 5;

        public ActionResult Index(int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel();
            IEnumerable<ProductViewModel> productList = ProductViewModel.Mapping(Product.ProductList);
            model.ProductList = productList;

            int pageIndex = page < 1 ? 1 : page;
            model.PageIndex = pageIndex;

            model.ProductList = model.ProductList.ToPagedList(pageIndex, pageSize);

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ProductListViewModel model)
        {
            IEnumerable<ProductViewModel> productList = ProductViewModel.Mapping(Product.ProductList);

            var _productList = from p in productList
                               select p;

            if (!string.IsNullOrEmpty(model.InputLocale))
            {
                _productList = _productList.Where(p => p.Locale == model.InputLocale);
            }
            if (!string.IsNullOrEmpty(model.InputProductName))
            {
                _productList = _productList.Where(p => p.Product_Name.ToLower().Contains(model.InputProductName.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.InputPriceMax))
            {
                _productList = _productList.Where(p => p.Price >= Convert.ToDecimal(model.InputPriceMax));
            }
            if (!string.IsNullOrEmpty(model.InputPriceMin))
            {
                _productList = _productList.Where(p => p.Price <= Convert.ToDecimal(model.InputPriceMin));
            }

            model.ProductList = _productList;


            int pageIndex = model.PageIndex < 1 ? 1 : model.PageIndex;
            model.PageIndex = model.PageIndex < 1 ? 1 : model.PageIndex;

            model.ProductList = model.ProductList.ToPagedList(pageIndex, pageSize);

            return View(model);
        }


        public JsonResult GetLocale()
        {
            string json = "";
            List<Locale> locales = Locale.Data;
            json = JsonConvert.SerializeObject(locales);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int? Id)
        {
            return View();
        }
    }
}