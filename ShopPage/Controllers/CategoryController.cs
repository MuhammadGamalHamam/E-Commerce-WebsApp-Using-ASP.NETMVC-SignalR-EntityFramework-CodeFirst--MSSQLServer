using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopPage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        // GET: Category
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
           // return View(context.Categories.ToList());
            return PartialView(context.Categories.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return PartialView(context.Categories.FirstOrDefault(p => p.ID == id));

            //return View(context.Categories.FirstOrDefault(p => p.ID == id));
        }
       
        
        [HttpPost]//*************************************************
        public ActionResult Edit(Category pro)
        {
            if (ModelState.IsValid)
            {
                var found = context.Categories.FirstOrDefault(x => x.ID == pro.ID);
                if (found != null)
                {
                    var Categorie = context.Categories.FirstOrDefault(p => p.ID == pro.ID);
                    Categorie.Name = pro.Name;
                    Categorie.Active = pro.Active;
                    Categorie.Description = pro.Description;
                    Categorie.Picture = pro.Picture;

                    context.SaveChanges();
                    //return RedirectToAction("Index");
                    return Content("<script>alert('Edited successfully');</script>");
                }
                else
                {
                    ModelState.AddModelError("", "'plz check your data is valid'");
                    return Content("<script>alert('plz check your data is valid');</script>");
                    //return View(pro);

                    //return View(pro);
                }
            }
            //return View(pro);
            return PartialView(pro);

        }
        
        
        [HttpGet]
        public ActionResult Details(int id)
        {
            return PartialView(context.Categories.FirstOrDefault(p => p.ID == id));

           // return View(context.Categories.FirstOrDefault(p => p.ID == id));
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category Category = context.Categories.Find(id);
            if (Category == null)
            {
                return HttpNotFound();
            }
            else
            {
                //return View(Category);
                context.Categories.Remove(Category);
                context.SaveChanges();
                return Content("<script>alert('deleted successfully');</script>");
            }
        }


        [HttpGet]
        public ActionResult Create()
        {

            //return View();
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Category pro)
        {
            if (ModelState.IsValid)
            {
                var found = context.Categories.FirstOrDefault(x => x.Name == pro.Name);
                if (found == null)
                {
                    context.Categories.Add(pro);
                    context.SaveChanges();
                   // return RedirectToAction("Index");
                    return Content("<script>alert('added');</script>");
                }
                else
                {
                    ModelState.AddModelError("", "this Category is already exists");
                    //return View(pro);
                    // return PartialView(pro);
                    return Content("<script>alert('error happened');</script>");

                }
            }
            return View(pro);
            //return View(pro);
        }


    }
}