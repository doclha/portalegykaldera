using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal_v1._0._1.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Page404(string aspxerrorpath)
        {
            if (!string.IsNullOrEmpty(aspxerrorpath))
                ViewBag.Kaynak = aspxerrorpath;
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}