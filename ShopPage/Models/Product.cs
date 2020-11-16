using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopPage
{
    [Table("Product")]
    public class Product
    {
        public int ID { get; set; }

        [Required] //m
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }//m

        public virtual ICollection<Item> Items { get; set; }
        
        public string AvailableColors { get; set; }
        public string AvailableSizes { get; set; }
        public bool ProductAvailable { get; set; } //defualt-m
        public bool ProductDiscount { get; set; } //defualt-m
    }
}