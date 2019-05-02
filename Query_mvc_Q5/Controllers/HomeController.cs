using Newtonsoft.Json;
using Query_mvc_Q5.Models;
using Query_mvc_Q5.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace Query_mvc_Q5.Controllers
{
    public class HomeController : Controller
    {
        private int pageSize = 5;
        public ActionResult Index(int? page, string slctLocale, string inputPrdtName, decimal? inputPriceHr)
        {
            ProductListViewModel productListViewModel = new ProductListViewModel();
            List<ProductViewModel> productList = GetProductsFromJsonFile();

            var _productList = from p in productList
                               where p.Locale == slctLocale &&
                               p.Product_Name == inputPrdtName &&
                               p.Price >= inputPriceHr
                               select p;

            productListViewModel.ProductList = _productList;

            var pageNumber = page ?? 1;
            productListViewModel.ProductList = productListViewModel.ProductList.ToPagedList(pageNumber, pageSize);

            return View(productListViewModel);
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
    }
}