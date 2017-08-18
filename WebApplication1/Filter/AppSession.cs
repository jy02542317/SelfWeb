using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filter
{
    public class AppSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["My_user"] == null)
            {
                HttpContext.Current.Response.Redirect("/Login");
            
            }
        }
    }

    //public class AuthenAdminAttribute : FilterAttribute, IAuthenticationFilter
    //{
    //    public void OnAuthentication(AuthenticationContext filterContext)
    //    {
    //        //这个方法是在Action执行之前调用
    //        var user = filterContext.HttpContext.Session["AdminUser"];
    //        if (user == null)
    //        {
    //            //filterContext.HttpContext.Response.Redirect("/Account/Logon");
    //            var Url = new UrlHelper(filterContext.RequestContext);
    //            var url = Url.Action("Logon", "Account", new { area = "" });
    //            filterContext.Result = new RedirectResult(url);
    //        }
    //    }
    //    public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
    //    {
    //        //这个方法是在Action执行之后调用
    //    }
    //}
}