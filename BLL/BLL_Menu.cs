using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Model;

namespace BLL
{
    public class BLL_Menu
    {
        private DAO_Menu dao = new DAO_Menu();

        public int Add(Menu t)
        {
            return dao.insert(t);
        }

        public bool Exist(Menu t)
        {
            return dao.exist(t);
        }

        public Menu GetModelbyId(int id)
        {
            Menu menu = new Menu();
            menu.id = id;
            return dao.get_model_by_PK(menu);
        }

        public Menu GetModelbyCondition(Menu menu)
        {
            return dao.get_model_by_condition(menu);
        }

        public int UpdateModelbyPK(Menu menu)
        {
            return dao.update_model_by_PK(menu);
        }

        public List<Menu> GetModelListbyCondition(Menu menu)
        {
            return dao.get_model_list_by_condition(menu);
        }

        public int UpdateModelbyCondition(Menu menu, Menu condition)
        {
            return dao.update_model_by_Condition(menu, condition);
        }

        public List<Menu> GetMenu(int level)
        {
            return dao.getMenu(level);
        }

        public List<Menu> GetMenu()
        {
            return dao.getMenu();
        }
    }
}
