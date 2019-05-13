using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ASP_NET_MVC_Q5.Models
{
    public class Product
    {
        private const string _dataSource = "@~/App_Data/data.json";

        private static string _sourcePath = HttpContext.Current.Server.MapPath(_dataSource);

        public int Id { get; set; }

        public string Locale { get; set; }

        public string Product_Name { get; set; }

        public decimal Price { get; set; }

        public string Promote_Price { get; set; }

        public DateTime Create_Date { get; set; }

        public static IEnumerable<Product> _productList;
        public static IEnumerable<Product> ProductList
        {
            get
            {
                _productList = GetProductList();
                return _productList;
            }
        }

        private static IEnumerable<Product> GetProductList()
        {
            IEnumerable<Product> productList = null;

            using (StreamReader sr = new StreamReader(_sourcePath))
            {
                string json = sr.ReadToEnd();
                IEnumerable<Product> items = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                productList = items;
            }

            return productList;
        }
    }
}