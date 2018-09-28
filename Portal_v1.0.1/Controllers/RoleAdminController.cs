using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Portal_v1._0._1.Identity;
using Portal_v1._0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal_v1._0._1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<PortalUser> userManager;
        public RoleAdminController()
        {
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDataContext()));
            var userStore = new UserStore<PortalUser>(new IdentityDataContext());
            userManager = new UserManager<PortalUser>(userStore);
        }
        // GET: RoleAdminA
        public ActionResult Index()
        {
            return View(roleManager.Roles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name)
        {

            if (ModelState.IsValid)
            {
                var result = roleManager.Create(new IdentityRole(name));
                if (result.Succeeded)
                {
                    TempData["Success"] = "Rol eklendi !";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(name);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var role = roleManager.FindById(id);
            if (role != null)
            {
                var result = roleManager.Delete(role);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Rol silindi !";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Rol Bulunamadı" });
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var role = roleManager.FindById(id);

            var members = new List<PortalUser>();
            var nonmembers = new List<PortalUser>();

            foreach (var user in userManager.Users.ToList())
            {
                var list = userManager.IsInRole(user.Id, role.Name) ? members : nonmembers;

                list.Add(user);
            }

            return View(new RoleEditModel()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            });
        }

        [HttpPost]
        public ActionResult Edit(RoleUpdateModel model)
        {

            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    var user = userManager.Users.FirstOrDefault(i => i.Id == userId);
                    result = userManager.AddToRole(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }





                    else if (result.Succeeded)
                    {

                        TempData["Success"] = user.Name + " " + user.LastName + " İsimli kullanıcının rolü güncellendi !";
                    }
                }


                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = userManager.Users.FirstOrDefault(i => i.Id == userId);
                    result = userManager.RemoveFromRole(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                    else if (result.Succeeded)
                    {
                        TempData["Success"] = user.Name + " " + user.LastName + " İsimli kullanıcısının rolü güncellendi !";
                    }
                }
                return RedirectToAction("Index");
            }
            return View("Error", new string[] { "Aranılan rol yok" });
        }
    }
}
