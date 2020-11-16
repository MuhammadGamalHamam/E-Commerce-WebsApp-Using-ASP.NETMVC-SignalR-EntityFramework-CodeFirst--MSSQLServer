using ShopPage.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopPage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        ApplicationDbContext DBcontext = new ApplicationDbContext();

        //mariam //show all orders from dashboard

        public ActionResult ShowAll()
        {
            var ordersList = new List<Order_ShowAll_ViewModel>();
            var ordersDB = DBcontext.Orders.ToList();

            foreach (var item in ordersDB)
            {
                var newOrder = new Order_ShowAll_ViewModel
                {   ID=item.ID,
                    UserName = DBcontext.Users.FirstOrDefault(u => u.Id == item.UserID).UserName,
                    OrderNumber = item.OrderNumber,
                    SalesTax = item.SalesTax,
                    Freight = item.Freight,
                    TimeStamp = item.TimeStamp,
                    Fulfill = item.Fulfill,
                    Paid = item.Paid,

                };
                ordersList.Add(newOrder);
            }
            return PartialView(ordersList);
           // return View(ordersList);
        }

        public ActionResult Details(int id ) 
        {
            var orderDetalisList = DBcontext.OrderDetails.Where(o => o.OrderID==id).ToList();
            //return View(orderDetalisList);
            return PartialView(orderDetalisList);
        }


        public ActionResult Delete(int id) 
        {
            try
            {
                var order = DBcontext.Orders.FirstOrDefault(o => o.ID == id);
                DBcontext.Orders.Remove(order);
                DBcontext.SaveChanges();
                return Content("<script>alert('deleted successfully');</script>");


            }
            catch 
            {
                return Content("<script>alert('some error happend');</script>");
            }
        }
    }
}