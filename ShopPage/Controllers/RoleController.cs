using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopPage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        // GET: AllRoles
        public ActionResult AllRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var allRoles = roleManager.Roles.Select(r => new RoleModelView { RoleId = r.Id, RoleName = r.Name }).ToList();
            return View(allRoles);
        }


        // GET: AddRole
        public ActionResult AddRole()
        {
            return View();
        }

        // POST: AddRole
        [HttpPost]
        public ActionResult AddRole(RoleModelView roleModel)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (ModelState.IsValid)
            {
                if (!roleManager.RoleExists(roleModel.RoleName))
                {
                    var role = new IdentityRole
                    {
                        Name = roleModel.RoleName
                    };
                    var result = roleManager.Create(role);

                    if (result.Succeeded)
                    {
                        //ModelState.AddModelError("", "Role Added Successfully");
                        return RedirectToAction("AllRoles");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Role Already Exists");
                }
            }
            else
            {
                ModelState.AddModelError("", "You Have Some Errors.\nPlease, Fix it.");
            }

            return View(roleModel);
        }


        // GET: UpdateRole
        [HttpGet]
        public ActionResult UpdateRole(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var role = roleManager.Roles.Where(r => r.Id == id)
                                            .Select(r => new RoleModelView { RoleId = r.Id, RoleName = r.Name })
                                            .FirstOrDefault();
            
            return View(role);
        }

        // POST: UpdateRole
        [HttpPost]
        public ActionResult UpdateRole(RoleModelView roleModel, string id)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

                if (!roleManager.RoleExists(roleModel.RoleName))
                {
                    var role = roleManager.FindById(id);
                    role.Name = roleModel.RoleName;
                    var result = roleManager.Update(role);

                    if (result.Succeeded)
                    {
                        //ModelState.AddModelError("", "Role Added Successfully");
                        return RedirectToAction("AllRoles");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "There is another Role has the same Name");
                }
            }

            return View(roleModel);
        }


        // GET: DeleteRole
        [HttpGet]
        [ActionName("DeleteRole")]
        public ActionResult Delete_Get(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var role = roleManager.Roles
                        .Where(r => r.Id == id)
                        .Select(r => new RoleModelView { RoleId = r.Id, RoleName = r.Name })
                        .FirstOrDefault();

            return View(role);
        }

        // POST: DeleteRole
        [HttpPost]
        [ActionName("DeleteRole")]
        public ActionResult Delete_Post(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var role = roleManager.FindById(id);

            var result = roleManager.Delete(role);

            if (result.Succeeded)
            {
                //ModelState.AddModelError("", "Role Added Successfully");
                return RedirectToAction("AllRoles");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item);
                }

                return View(id);
            }
        }


    }
}