using Microsoft.AspNet.Identity;
using ShopPage.Models;
using ShopPage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopPage.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext DBcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = new IndexViewModel();
            var womenList = (
                from i in DBcontext.Items join p in DBcontext.Products
                on i.ProductID equals p.ID
                where p.Category.Name == "women"
                select i).Take(3).ToList();
            var menList = (
                from i in DBcontext.Items join p in DBcontext.Products
                on i.ProductID equals p.ID
                where p.Category.Name == "men"
                select i).Take(3).ToList();

            model.WomenList = womenList;
            model.MenList = menList;

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        //mariam again 

        [Authorize]
        public ActionResult CheckOut()
        {
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();


            List<Item> items = new List<Item>();

            var orders =
                (
                 from o in DBcontext.Orders
                 where o.User.Id == id             // && o.TimeStamp=
                 select o).ToList(); //orderdetails is a collection

            if (orders != null)
            {
                var maxTime = orders.Max(o => o.TimeStamp.ToString());

                var orderDetails =
                (
                from o in orders
                where o.TimeStamp.ToString() == maxTime
                select o.OrderDetails.ToList()
                );


                if (orderDetails.Count() > 0)
                {
                    foreach (var item in orderDetails.FirstOrDefault())
                    {
                        items.Add(item.Item);
                    }
                }
            }

            return View(items);
        }
 

        [Authorize(Roles = "Admin")]
        public ActionResult DashBoard() 
        {
            return PartialView();
        }
    }

}
