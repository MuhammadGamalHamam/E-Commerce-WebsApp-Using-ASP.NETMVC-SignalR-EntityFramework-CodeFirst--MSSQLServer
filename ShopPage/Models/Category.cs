using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopPage
{
    [Table("Category")]
    public class Category
    {
        public int ID { get; set; }

        [Required] //m
        //unique-m
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string Picture { get; set; }
        public bool Active { get; set; } //default-m


        public override string ToString()
        {
            return this.Name;
        }
    }
} 