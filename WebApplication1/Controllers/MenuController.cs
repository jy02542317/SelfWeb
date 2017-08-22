using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using WebApplication1.Filter;

namespace WebApplication1.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        [AppSession]
        public ActionResult Index()
        {
            BLL_Menu menu = new BLL_Menu();
            List<Menu> list = menu.GetMenu();
            ViewBag.MenuList = list;
            return View();
        }

        [HttpPost]
        public JsonResult getMenu()
        {
            Account account = (Account)Session["My_User"];
            BLL_Menu menu = new BLL_Menu();
            List<Menu> list = menu.GetMenu(account.account_type);

            List<Menu> list0 = list.FindAll(c => c.parent == 0).OrderBy(c => c.id).ToList(); 
            List<Menu> result = new List<Menu>();
            foreach (Menu i in list0)
            {
                result.Add(i);
                List<Menu> list1 = list.FindAll(c => c.parent == i.id);
                if (list1 != null && list.Count > 0)
                {
                    foreach (Menu j in list1)
                    {
                        result.Add(j);
                    }
                }
            }
            string _json = JsonConvert.SerializeObject(result);
            return Json(_json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddMenu(string name,string url,int parentid)
        {
            BLL_Menu blm = new BLL_Menu();
            Menu menu = new Menu();
            menu.name = name;
            menu.url = url;
            menu.parent = parentid;
            blm.Add(menu);
            return Json("");
        }

        public string SimpleMenu(int id)
        {
            return "test";
        }
    }
}