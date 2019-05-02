using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Query_mvc_Q5.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Locale { get; set; }

        public string Product_Name { get; set; }

        public decimal Price { get; set; }

        public string Promote_Price { get; set; }

        public DateTime Create_Date { get; set; }
    }
}