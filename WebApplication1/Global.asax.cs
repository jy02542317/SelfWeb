using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            string url = AppDomain.CurrentDomain.BaseDirectory + "\\log4net.config";
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(url));
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError().GetBaseException();
            Type type=Server.GetLastError().GetType();
            var logger = LogManager.GetLogger(type);
            logger.Error("错误：", ex);
            Response.Redirect("/Home/Error");
        }
    }
}
