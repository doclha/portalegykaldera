using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Portal_v1._0._1.Identity;
using Portal_v1._0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal_v1._0._1.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<PortalUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController()
        {
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDataContext()));
            var userStore = new UserStore<PortalUser>(new IdentityDataContext());
            userManager = new UserManager<PortalUser>(userStore);

            userManager.UserValidator = new UserValidator<PortalUser>(userManager)
            {
                RequireUniqueEmail = true

            };

        }

        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new PortalUser();
                user.UserName = model.UserName;
                user.Email = model.Mail;
                user.Title = model.Title;
                user.Name = model.Name;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                user.IseGiris = model.iseGiris;
                user.CiktiMi = false;
                user.IsCikis = "";

                var result = userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");
                    ViewData["Success"] = "Başarıyla kaydedildi";

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.UserName, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Yanlış kullanıcı adı veya parola");

                }
                else
                {

                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true

                    };
                    authManager.SignOut();
                    authManager.SignIn(authProperties, identity);
                    var role = user.Roles.FirstOrDefault();
                    var role2 = roleManager.Roles.FirstOrDefault(i => i.Name == "Admin");
                    if (role.RoleId == role2.Id)
                    {
                        TempData["Warning"] = MvcHtmlString.Create("Portal v1.0.2 Güncellemesi:"+"<br>"+ "- Parola Değiştir sayfası eklenmiştir. ( Uyarı : Parolanızı değiştirmeden önce mail adresiniz tanımlı değilse tanımlamayı unutmayınız! )");
                        return Redirect("/Admin/Index");
                    }
                    else
                    {
                        TempData["Warning"] = MvcHtmlString.Create("Portal v1.0.2 Güncellemesi:" + "<br>" + "- Parola Değiştir sayfası eklenmiştir. ( Uyarı : Parolanızı değiştirmeden önce mail adresiniz tanımlı değilse tanımlamayı unutmayınız! )");
                        return Redirect("/");
                    }

                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult PasswordChange()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult PasswordChange(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = userManager.FindById(User.Identity.GetUserId());
            var authManager = HttpContext.GetOwinContext().Authentication;
            var identity = userManager.CreateIdentity(user, "ApplicationCookie");
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = true

            };
            var result =  userManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                
                if (user != null)
                {
                     authManager.SignIn(authProperties, identity);
                }
                TempData["Success"] = "Parola Değiştirildi";
                return View();
            }

            TempData["Message"] = "Bir hata oluştu !!!(Mail adresinizin ekli olup olmadığını kontrol edin !)";
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = userManager.FindById(User.Identity.GetUserId());
            var authManager = HttpContext.GetOwinContext().Authentication;
            var identity = userManager.CreateIdentity(user, "ApplicationCookie");
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = true

            };
            var result = userManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {

                if (user != null)
                {
                    authManager.SignIn(authProperties, identity);
                }
                TempData["Success"] = "Parola Değiştirildi";
                return View();
            }

            TempData["Message"] = "Bir hata oluştu !!!(Mail adresinizin ekli olup olmadığını kontrol edin !)";
            return View(model);
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                //Hata kontolü
                return RedirectToAction("Index", "Admin", null);
            }
            IdentityDataContext db = new IdentityDataContext();
            var user = userManager.Users.FirstOrDefault(i => i.Id == id);
            var cv = db.CVler.Where(i => i.PortalUserId == user.Id);
            foreach (var item in cv)
            {
                db.CVler.Remove(item);
            }
            db.SaveChanges();
            userManager.Delete(user);
           

             TempData["Success"] = "Kullanıcı silindi !";
            return RedirectToAction("Index", "Admin", null);
        }
    }
}