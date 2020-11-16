using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace ShopPage.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        //
        // GET: Login
        //
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: Login
        //
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            //ApplicationDbContext context = new ApplicationDbContext();
            //UserStore<IdentityUser> store = new UserStore<IdentityUser>(context);
            //UserManager<IdentityUser> manager = new UserManager<IdentityUser>(store);
            if (ModelState.IsValid)
            {
                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(
                                                        new UserStore<IdentityUser>(
                                                            new ApplicationDbContext()));
                //IdentityUser user = manager.FindByName(login.Name);
                IdentityUser user = manager.Find(login.Name, login.Password);
                //if (manager.CheckPassword(user, login.Password))
                if (user != null)
                {
                    //Create Cookie
                    SignInManager<IdentityUser, string> signIn =
                        new SignInManager<IdentityUser, string>(manager, HttpContext.GetOwinContext().Authentication);
                    signIn.SignIn(user, true, login.RememberMe);//the last one is the value of remember me checkbox

                    //Redirect to Action
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "UserName or Password not correct!");
            }

            return View(login);
        }


        //
        // GET: Register
        //
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: Register
        //
        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new ApplicationDbContext()));

                IdentityUser user = new IdentityUser
                {
                    UserName = register.Name,
                    Email = register.Email,
                };
                IdentityResult result = manager.Create(user, register.Password);
                if (result.Succeeded)
                {
                    // Add Role
                    manager.AddToRole(user.Id, "User");

                    //Create Cookie
                    SignInManager<IdentityUser, string> signIn =
                        new SignInManager<IdentityUser, string>(manager, HttpContext.GetOwinContext().Authentication);
                    signIn.SignIn(user, true, false);//the last one is the value of 'remember me' checkbox

                    //Redirect to Action
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }

            return View(register);
        }


        //
        // GET: Logout
        //
        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //----------------------------------------------------------------------------------------


        // GET: AllRoles
        [Authorize(Roles = "Admin")]
        public ActionResult AllUsers()
        {
            //User.Identity.GetUserId();
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new ApplicationDbContext()));
            var allUsers = userManager.Users.Select(r => new UserModelView
            {
                UserID = r.Id,
                Name = r.UserName,
                Email = r.Email
            }).ToList();

            return PartialView(allUsers);
        }


        // GET: AddUser
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            ViewBag.MyRoles = roleManager.Roles.ToList();

            return PartialView();
        }

        // POST: AddUser
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser(UserModelView userModel)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (ModelState.IsValid)
            {
                bool sign = false;
                foreach (var item in userManager.Users.Select(u => u.UserName).ToList())
                {
                    if (userModel.Name.ToLower() == item.ToLower())
                        sign = true;
                }

                if (sign)
                {
                    ModelState.AddModelError("", "This Username Already Exists");
                }
                else
                {
                    var user = new IdentityUser
                    {
                        UserName = userModel.Name,
                        Email = userModel.Email
                    };
                    string userPass = userModel.Password;

                    var result = userManager.Create(user, userPass);
                    if (result.Succeeded)
                    {
                        var role = roleManager.FindById(userModel.UserRoleID);
                        userManager.AddToRole(user.Id, role.Name);

                        //return RedirectToAction("AllUsers");
                        return Content("<script>alert('Added successfully');</script>");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You Have Some Errors.\nPlease, Fix it.");
            }

            ViewBag.MyRoles = roleManager.Roles.ToList();
            return View(userModel);
        }


        // GET: DeleteUser
        [HttpGet]
        [ActionName("DeleteUser")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete_Get(string id)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new ApplicationDbContext()));
            var user = userManager.Users
                        .Where(r => r.Id == id)
                        .Select(r => new UserModelView
                        {
                            UserID = r.Id,
                            Name = r.UserName,
                            Email = r.Email
                        })
                        .FirstOrDefault();

            return View(user);
        }


        // POST: DeleteUser
        [HttpPost]
        [ActionName("DeleteUser")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete_Post(string id)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new ApplicationDbContext()));

            var user = userManager.FindById(id);

            var result = userManager.Delete(user);

            if (result.Succeeded)
            {
               // return RedirectToAction("AllUsers");
                return Content("<script>alert('Deleted successfully');</script>");
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