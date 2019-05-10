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

        public ActionResult Index(int? page, ProductListViewModel model)
        {
            List<ProductViewModel> productList = GetProductsFromJsonFile();

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


            var pageNumber = page ?? 1;
            model.ProductList = model.ProductList.ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        [HttpPost]
        public ActionResult Query(ProductListViewModel model)
        {
            return View(model);
        }


        public JsonResult GetLocale()
        {
            string json = "";
            List<Locale> locales = Locale.Data;
            json = JsonConvert.SerializeObject(locales);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        private List<ProductViewModel> GetProductsFromJsonFile()
        {
            List<ProductViewModel> productListViewModels = null;

            string FileName = @"data.json";

            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/" + FileName)))
            {
                string json = sr.ReadToEnd();
                List<ProductViewModel> items = JsonConvert.DeserializeObject<List<ProductViewModel>>(json);
                productListViewModels = items;
            }

            return productListViewModels;
        }

        public ActionResult Detail(int? Id)
        {
            return View();
        }
    }
}