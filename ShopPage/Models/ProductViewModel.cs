using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopPage.Models
{
    public class ProductViewModel
    {

        [Required] 
        public string Name { get; set; }
       
        public string Description { get; set; }


        [Required]
        public string CategoryName { get; set; }

        public string AvailableColors { get; set; }

        [Required]
        public string AvailableSizes { get; set; }
        public bool ProductAvailable { get; set; } 
        public bool ProductDiscount { get; set; } 
    }
}