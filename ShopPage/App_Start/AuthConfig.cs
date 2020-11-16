using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopPage
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext() : base("ShopCS")
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Product> Products { get; set; }

       // public System.Data.Entity.DbSet<ShopPage.ViewModel.Order_ShowAll_ViewModel> Order_ShowAll_ViewModel { get; set; }
        //public virtual DbSet<Category> Categories { get; set; }
    }
}