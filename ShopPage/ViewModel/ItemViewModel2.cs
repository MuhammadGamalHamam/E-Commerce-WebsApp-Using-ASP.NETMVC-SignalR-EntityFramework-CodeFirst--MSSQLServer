using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopPage.ViewModel
{
    public class ItemViewModel2
    {

        [Required]
        public List<SelectListItem> Products = new List<SelectListItem>();

        [Required]
        public List<SelectListItem> Categories = new List<SelectListItem>();

        [Required]
        [DisplayName("Category Name")]
        public int CategoryID { get; set; }

        [DisplayName("Product Name")]
        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public string Color { get; set; }

        [Range(0.0, 100.0)]
        public double Discount { get; set; }

        [Required]
        public double Price { get; set; }

        public int UnitsInStock { get; set; }

        [Required]
        public string Picture { get; set; }

        [Range(1.0, 5.0)]
        public double Rank { get; set; }

        public string Note { get; set; }
    }
}