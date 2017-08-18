using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Util;
using System.Reflection;
using System.Data.SqlClient;
using Util.Attribute;
using System.Data;

namespace DAO
{
    public class DAO_Menu : BaseDao<Menu>
    {
        private const string TableName = "Menu";
        public int insert(Menu t)
        {
            return Insert<Menu>(TableName, t, true);
        }

        public bool exist(Menu t)
        {
            return Exist<Menu>(TableName, t);
        }

        public Menu get_model_by_PK(Menu t)
        {
            return GetModelbyPK<Menu>(TableName, t);
        }

        public Menu get_model_by_condition(Menu t)
        {
            return GetModelbyCondition<Menu>(TableName, t);
        }

        public List<Menu> get_model_list_by_condition(Menu t)
        {
            return GetListbyCondition<Menu>(TableName, t);
        }

        public int update_model_by_PK(Menu t)
        {
            return UpdateModelbyPK<Menu>(TableName, t);
        }

        public int update_model_by_Condition(Menu t, Menu condition)
        {
            return UpdateModelbyCondition<Menu>(TableName, t, condition);
        }

        public List<Menu> getMenu(int level)
        {
            SqlParameter param = new SqlParameter("@account_type_id", level);
            SqlParameter[] sql = new SqlParameter[1];
            sql[0] = param;
            DataSet ds = DBHelper.GetDataSet("Menu_Select", sql, CommandType.StoredProcedure);
            if(ds!=null &&ds.Tables.Count>0)
            {
                IList<Menu> list= DataTableToList<Menu>(ds.Tables[0]);
                return list.ToList();
            }
            else{
                return null;
            }  
        }

        public List<Menu> getMenu()
        {
            DataSet ds = DBHelper.GetDataSet("select * from Menu");
            if (ds != null && ds.Tables.Count > 0)
            {
                IList<Menu> list = DataTableToList<Menu>(ds.Tables[0]);
                return list.ToList();
            }
            else
            {
                return null;
            }  
        }
    }
}
