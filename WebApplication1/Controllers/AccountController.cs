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
    [AppSession]
    public class AccountController : Controller
    {
      
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            BLL_Account bll = new BLL_Account();
            Account t = (Account)Session["My_user"];
            string newpassword = Request.Form["newpassword"].ToString();
            string confirmpassword = Request.Form["confirmpassword"].ToString();
            string oldpassword = Request.Form["oldpassword"].ToString();
            string message = string.Empty;
            string script = string.Empty;
            if (bll.CheckPassword(t, oldpassword, newpassword, confirmpassword, out message))
            {
                Session.Remove("My_user");
                script = string.Format("alert('{0}');returnParent();", message.Trim());
            }
            else
            {
                script = string.Format("alert('{0}');", message.Trim());
            }
            return JavaScript(script);
        }
    }
}