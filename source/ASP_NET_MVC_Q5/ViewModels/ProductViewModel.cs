using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_NET_MVC_Q5.ViewModels
{
    public class ProductListViewModel
    {
        public string InputLocale { get; set; }
        public string InputProductName { get; set; }
        public string InputPriceMax { get; set; }
        public string InputPriceMin { get; set; }

        public IEnumerable<ProductViewModel> ProductList { get; set; }
    }

    public class ProductViewModel
    {
        [Display(Name = "產品唯一代號")]
        public int Id { get; set; }

        [Display(Name = "地區")]
        public string Locale { get; set; }

        [Display(Name = "產品名稱")]
        public string Product_Name { get; set; }

        [Display(Name = "價格")]
        public decimal Price { get; set; }

        [Display(Name = "促銷價格")]
        public string Promote_Price { get; set; }

        [Display(Name = "建立時間")]
        public string Create_Date { get; set; }
    }
}