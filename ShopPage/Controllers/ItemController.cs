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
   // [Authorize(Roles = "Admin")]
    public class ItemController : Controller
    {
        ApplicationDbContext DBcontext = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var item = DBcontext.Items.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                // return View(item);
                return PartialView(item);
            }
            // return RedirectToAction("index", "home"); //not found 
            return HttpNotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Item i)
        {

            if (ModelState.IsValid)
            {
                var item = DBcontext.Items.FirstOrDefault(x => x.ID == i.ID);
                if (item != null)
                {
                    item.Size = i.Size;
                    item.Color = i.Color;
                    item.Discount = i.Discount;
                    item.Price = i.Price;
                    item.UnitsInStock = i.UnitsInStock;
                    item.Picture = i.Picture;
                    item.Rank = i.Rank;
                    item.Note = i.Note;
                    item.Size = i.Size;
                    DBcontext.SaveChanges();
                    return Content("<script>alert('edited successfully');</script>");
                }
                //return View();
                return PartialView(item);
            }
            else
            {
                ModelState.AddModelError("", "plz check that your data is valid");
                //return View(i);
                return View(i);
            }

        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var item = DBcontext.Items.FirstOrDefault(x => x.ID == id);
            var p = DBcontext.Products.FirstOrDefault(x => x.ID == item.ProductID);
            var c = DBcontext.Categories.FirstOrDefault(x => x.ID == p.CategoryID);
            ViewBag.prodName = p.Name;
            ViewBag.catName = c.Name;
            //  return View(item);
            return PartialView(item);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ShowAll()
        {
            var AllItems = DBcontext.Items.ToList();
            // return View(AllItems);
            return PartialView(AllItems);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var item = DBcontext.Items.FirstOrDefault(x => x.ID == id);
            if (item != null)
            {
                try
                {
                    DBcontext.Items.Remove(item);
                    DBcontext.SaveChanges();
                    // return RedirectToAction("ShowAll", "item");
                    return Content("<script>alert('Deleted Successfully');</script>");
                }
                catch
                {
                    //  return View("edit", id);
                    return Content("<script>alert('Some error happened cannot delete this item');</script>");
                };
            }
            // return RedirectToAction("ShowAll", "item");
            return Content("<script>alert('this item was not exist any more');</script>");
        }


        //ajax request does not have view
        [Authorize(Roles = "Admin")]
        public ActionResult FillProducts(int? id)
        {


            var products = (
                  from p in DBcontext.Products
                  select p).ToList();

            List<ItemSelectedListViewModel> Prods = new List<ItemSelectedListViewModel>();
            foreach (var item in products)
            {

                Prods.Add(new ItemSelectedListViewModel
                {
                    ID = item.ID.ToString(),
                    Name = item.Name,
                    CategoryId = item.CategoryID
                });
            }

            return Json(Prods.Where(m => m.CategoryId == id), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            List<SelectListItem> Products = new List<SelectListItem>() {
                new SelectListItem() { Value="0", Text="-- Select Products --" },
           };

            List<SelectListItem> categories = new List<SelectListItem>();

            var MyItem = new ItemViewModel2();

            var catgs = (
                   from c in DBcontext.Categories
                   select c).ToList();

            foreach (var item in catgs)
            {
                categories.Add(new SelectListItem() { Value = (item.ID).ToString(), Text = item.Name });
            }

            MyItem.Products = Products;
            MyItem.Categories = categories;
            // return View(MyItem);
            return PartialView(MyItem);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult New(ItemViewModel2 i)
        {
            //initial model 
            List<SelectListItem> Products = new List<SelectListItem>() {
                new SelectListItem() { Value="0", Text="-- Select Products --" },
           };

            List<SelectListItem> categories = new List<SelectListItem>();
            var MyItem = new ItemViewModel2();
            var catgs = (
                   from c in DBcontext.Categories
                   select c).ToList();

            foreach (var item in catgs)
            {
                categories.Add(new SelectListItem() { Value = (item.ID).ToString(), Text = item.Name });
            }

            MyItem.Products = Products;
            MyItem.Categories = categories;
            //////////////////////////////////
            if (ModelState.IsValid)
            {
                Item newI = new Item();

                //coping Product
                var cat = DBcontext.Categories.FirstOrDefault(c => c.ID == i.CategoryID);
                //if (cat != null)
                //{
                var prod = DBcontext.Products.FirstOrDefault
                    (p => p.ID == i.ProductID && p.CategoryID == cat.ID);

                newI.Product = prod;
                newI.ProductID = prod.ID;

                //coping Item
                newI.Size = i.Size;
                newI.Color = i.Color;
                newI.Discount = i.Discount;
                newI.Price = i.Price;
                newI.UnitsInStock = i.UnitsInStock;
                newI.Picture = i.Picture;
                newI.Rank = i.Rank;
                newI.Note = i.Note;

                DBcontext.Items.Add(newI);
                DBcontext.SaveChanges();
                // return View(MyItem);
                return Content("<script>alert('Added successfully');</script>");
                //}
                //return View(MyItem);
            }
            else
            {
                ModelState.AddModelError("", "plz check that your data is valid");
                i.Categories = categories;
                i.Products = Products;
                // return View(i);
                return Content("<script>alert('plz check that your data is valid');</script>");
            }



        }



        //goerge

        public bool AddCart(int id)
        {
            bool isAdd = false;
            var item = DBcontext.Items.FirstOrDefault(p => p.ID == id);

            if (Session["cart"] == null)
            {
                List<ItemQuentety> li = new List<ItemQuentety>();
                ItemQuentety prod = new ItemQuentety { item = item, count = 1 };
                if (prod.item.UnitsInStock >= prod.count)
                {
                    li.Add(prod);
                    Session["cart"] = li;
                    ViewBag.cart = li.Count();
                    Session["count"] = 1;
                    ViewBag.error = null;
                    isAdd = true;
                }
                else
                {
                    isAdd = false;
                    ViewBag.error = "item is out of Stock";
                }

            }
            else
            {
                List<ItemQuentety> li = (List<ItemQuentety>)Session["cart"];
                // bool fond = false;
                //foreach (var item1 in li)
                //{
                //    if (item1.item.ID == item.ID)
                //    {
                //        fond = true;
                //        item1.count += 1;
                //    }
                //}
                var itemSelected = li.FirstOrDefault(i => i.item.ID == item.ID);
                if (itemSelected == null)
                {
                    ItemQuentety prod = new ItemQuentety { item = item, count = 1 };
                    if (prod.item.UnitsInStock >= prod.count)
                    {
                        li.Add(prod);
                        ViewBag.error = null;
                        isAdd = true;
                    }
                    else
                    {
                        isAdd = false;
                        ViewBag.error = "item is out of Stock";
                    }
                }
                else
                {
                    if (itemSelected.item.UnitsInStock > itemSelected.count)
                    {
                        li.Remove(itemSelected);
                        itemSelected.count += 1;
                        li.Add(itemSelected);
                        isAdd = true;
                    }
                    else
                    {
                        isAdd = false;
                        ViewBag.error = "item is out of Stock";
                    }
                }

                Session["cart"] = li;
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                ViewBag.cart = li.Count();

            }

            //return RedirectToAction("Index", "Home");
            //return View("Cart", Session["cart"]);
            //return View();
            return isAdd;
        }

        private Double sum(List<ItemQuentety> itemQuenteties)
        {
            double sum = 0;
            foreach (var item in itemQuenteties)
            {
                sum += item.total;
            }
            return sum;
        }

        [AllowAnonymous]
        public ActionResult Cart()
        {
            List<ItemQuentety> li = (List<ItemQuentety>)Session["cart"];
            if (li == null)
            {
                ViewBag.count = 0;
            }
            else
            {
                ViewBag.count = li.Count();
                ViewBag.total = sum(li);
            }

            return View(li);
        }


        [HttpGet]
        [Authorize]
        public ActionResult Paiment(double total)
        {
            ViewBag.total = total;
            return View();

        }

        [HttpPost]
        //[Authorize]
        [Authorize(Roles = "User")]
        public ActionResult Paiment(string credecit)
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = System.Web.HttpContext.Current.User.Identity.GetUserId();
                //check cridet
                Order order = new Order();
                order.Paid = true;
                order.SalesTax = .16;
                order.UserID = id;
                order.TimeStamp = DateTime.Now;
                //order.total
                DBcontext.Orders.Add(order);
                DBcontext.SaveChanges();
                List<ItemQuentety> li = (List<ItemQuentety>)Session["cart"];
                //double totalOfPrice=0;
                foreach (var item1 in li)
                {
                    OrderDetails orderDetails = new OrderDetails
                    {
                        ItemID = item1.item.ID,
                        OrderID = order.ID,
                        Price = item1.item.Price,
                        Quantity = item1.count,
                        Discount = item1.item.Discount,
                    };
                    orderDetails.Total = (orderDetails.Price - (orderDetails.Price * orderDetails.Discount)) * orderDetails.Quantity;
                    //totalOfPrice+= orderDetails.Total;
                    DBcontext.OrderDetails.Add(orderDetails);
                    var IteminStock = DBcontext.Items.FirstOrDefault(i => i.ID == item1.item.ID);
                    IteminStock.UnitsInStock -= item1.count;


                  // DBcontext.SaveChanges();
                   
                   
                }
                Session["cart"] = null;
                return Content("<script>alert('YOur Payment done successfully');</script>");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize]
        public ActionResult ResultOfPayment(int id)
        {
            return View();
            //return RedirectToAction("Cart", "item");
        }

        [AllowAnonymous]
        public ActionResult DeleteItemFromCart(int id)
        {
            List<ItemQuentety> li = (List<ItemQuentety>)Session["cart"];
            var ditem = li.FirstOrDefault(i => i.item.ID == id);
            li.Remove(ditem);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            return RedirectToAction("Cart", "item");
        }
    } 
}