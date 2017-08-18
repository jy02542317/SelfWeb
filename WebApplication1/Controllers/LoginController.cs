using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account t)
        {
            BLL_Account bll = new BLL_Account();
            Account account = null;
            account = bll.GetModelbyCondition(t);
            if (account != null && !string.IsNullOrEmpty(account.username))
            {
                Session["My_user"] = account;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Index");
            }

        }
    }
}