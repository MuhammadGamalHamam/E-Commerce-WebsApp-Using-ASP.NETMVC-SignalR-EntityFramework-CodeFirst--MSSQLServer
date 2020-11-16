using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopPage
{
    [Table("Item")]
    public class Item
    {
        public int ID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        [Required] //m
        public int Size { get; set; }

        [Required] //m
        public string Color { get; set; }

        [Range(0.0,100.0)]
        public double Discount { get; set; }
       
        [Required] //m
        public double Price { get; set; }
        public int UnitsInStock { get; set; }
        
        [Required] //m
        public string Picture { get; set; }

        [Range(1.0,5.0)]
        public double Rank { get; set; }

        public string Note { get; set; }

        //string Brand, string Name
        public string Brand { get; set; }
        public string Name { get; set; }


    }
}