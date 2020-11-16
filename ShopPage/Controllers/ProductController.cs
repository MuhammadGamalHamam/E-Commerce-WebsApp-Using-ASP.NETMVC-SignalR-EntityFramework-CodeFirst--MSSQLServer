using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopPage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        // GET: Product
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            //return View(context.Products.ToList());
            return PartialView(context.Products.ToList());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryID = new SelectList(context.Categories, "ID", "Name");
            //return View(context.Products.FirstOrDefault(p => p.ID == id));
            return PartialView(context.Products.FirstOrDefault(p => p.ID == id));
        }
        
        [HttpPost]
        public ActionResult Edit(Product pro)
        {
            if (ModelState.IsValid)
            {
             
                var found = context.Products.FirstOrDefault(x => x.ID == pro.ID && x.CategoryID == pro.CategoryID);
                if (found != null || found.ID == pro.ID)
                {
                    var prodect = context.Products.FirstOrDefault(p => p.ID == pro.ID);
                    prodect.Name = pro.Name;
                    prodect.CategoryID = pro.CategoryID;
                    prodect.Description = pro.Description;
                    prodect.ProductDiscount = pro.ProductDiscount;
                    prodect.ProductAvailable = pro.ProductAvailable;
                    context.SaveChanges();
                    //return RedirectToAction("/Index");
                    return Content("<script>alert('Edited successfully');</script>");
                
                }
                else
                {
                    
                    ViewBag.CategoryID = new SelectList(context.Categories, "ID", "Name");
                    //return PartialView(pro);
                    return Content("<script>alert('some thing went wrong pleaz try again');</script>");
                }
            }
            ModelState.AddModelError("", "Plz check that you entered valid data");
            return PartialView(pro);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            //return View(context.Products.FirstOrDefault(p => p.ID == id));
            return PartialView(context.Products.FirstOrDefault(p => p.ID == id));

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var prodect = context.Products.FirstOrDefault(p => p.ID == id);
            context.Products.Remove(prodect);
            context.SaveChanges();
           // return RedirectToAction("/Index");
            return Content("<script>alert('Deleted successfully');</script>");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(context.Categories, "ID", "Name");
            return PartialView();

 //           return View();
        }
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            if (ModelState.IsValid)
            {
                var found = context.Products.FirstOrDefault(x => x.Name == pro.Name &&x.CategoryID==pro.CategoryID);
                if (found == null)
                {
                    context.Products.Add(pro);
                    context.SaveChanges();
                   // return RedirectToAction("/Index");
                    return Content("<script>alert('Added successfully');</script>");
                }
                else
                {
                    ModelState.AddModelError("", "this product is already exists");
                    ViewBag.CategoryID = new SelectList(context.Categories, "ID", "Name");
                    //return View(pro);
                    return Content("<script>alert('this product is already exists');</script>");

                }
            }
            return View(pro);
            

        }

    }
}