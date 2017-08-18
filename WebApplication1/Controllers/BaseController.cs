using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pagination()
        {
            return View();
        }
	}
}