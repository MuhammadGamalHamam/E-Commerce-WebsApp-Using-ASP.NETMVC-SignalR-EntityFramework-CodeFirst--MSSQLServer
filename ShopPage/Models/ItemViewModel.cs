using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopPage.Models
{
    public class ItemViewModel
    {
        [Required]
        public String ProductName { get; set; }

        [Required]
        public String CategoryName { get; set; }



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