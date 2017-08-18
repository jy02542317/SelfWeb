using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using WebApplication1.Filter;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [AppSession]
        public ActionResult Index()
        {
            return View();
        }

        [AppSession]
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}