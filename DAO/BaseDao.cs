using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using System.Reflection;
using System.Data.SqlClient;
using Util.Attribute;
using System.Data;

namespace DAO
{
    public class BaseDao<Models> where Models : class
    {
        protected int Insert<T>(string TableName, T t, bool return_id)
        {
            int result = 0;

            Type type = t.GetType();
            PropertyInfo[] ps = type.GetProperties();

            StringBuilder InsertSQL = new StringBuilder();
            StringBuilder InsertColumn = new StringBuilder();
            StringBuilder InsertParameter = new StringBuilder();

            List<SqlParameter> param = new List<SqlParameter>();

            int count = 0;

            foreach (PropertyInfo i in ps)
            {
                count++;
                object[] attributes = i.GetCustomAttributes(typeof(PK), true);
                if (attributes.Count() == 0)
                {
                    object obj = i.GetValue(t, null);
    
                    if (obj == null)
                    {//去掉空属性
                        continue;
                    }
                    if (i.PropertyType == typeof(DateTime))
                    {//时间属性初始化时未赋值会变为默认最小值
                        DateTime dt;
                        DateTime.TryParse(obj.ToString(), out dt);
                        if (dt <= DateTime.MinValue)
                            continue;
                    }
                    string name = i.Name;
                    InsertColumn.Append(name);
                    InsertParameter.Append('@' + name);
                    param.Add(new SqlParameter('@' + name, obj));

                    if (count != ps.Count())
                    {
                        InsertColumn.Append(',');
                        InsertParameter.Append(',');
                    }
                }
            }
            InsertSQL.Append(string.Format(@"Insert into {0} ({1}) values ({2}); ", TableName, InsertColumn.ToString(), InsertParameter.ToString()));

            try
            {
                if (return_id)
                {
                    //返回id
                    InsertSQL.Append("select @@Identity");
                    result = DBHelper.ExcuteScalarSQL(InsertSQL.ToString(), param.ToArray());
                }
                else
                {
                    //返回行数
                    result = DBHelper.ExcuteSQL(InsertSQL.ToString(), param.ToArray());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        protected bool Exist<T>(string TableName, T t)
        {
            Type type = t.GetType();
            PropertyInfo[] ps = type.GetProperties();

            StringBuilder ExistSQL = new StringBuilder();
            StringBuilder WhereColumn = new StringBuilder();

            List<SqlParameter> param = new List<SqlParameter>();
            WhereColumn.Append(" Where 1=1 ");

            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(t, null);

                if (obj == null)
                {//去掉空属性
                    continue;
                }
                if (i.PropertyType == typeof(DateTime))
                {//时间属性初始化时未赋值会变为默认最小值
                    DateTime dt;
                    DateTime.TryParse(obj.ToString(), out dt);
                    if (dt <= DateTime.MinValue)
                        continue;
                }
                if (i.PropertyType == typeof(int))
                {
                    if (int.Parse(obj.ToString()) == 0)
                        continue;
                }

                string name = i.Name;
                WhereColumn.Append(string.Format(" And " + name + " = @" + name));
                param.Add(new SqlParameter('@' + name, obj));
            }

            ExistSQL.Append(string.Format(@"Select top 1 * from {0} {1}", TableName, WhereColumn));
            DataSet ds = DBHelper.GetDataSet(ExistSQL.ToString(), param.ToArray());

            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected T GetModelbyPK<T>(string TableName, T t)
        {
            Type type = t.GetType();
            PropertyInfo[] ps = type.GetProperties();

            StringBuilder SQL = new StringBuilder();
            StringBuilder WhereColumn = new StringBuilder();

            List<SqlParameter> param = new List<SqlParameter>();

            WhereColumn.Append(" Where 1=1 ");

            foreach (PropertyInfo i in ps)
            {
                object[] attributes = i.GetCustomAttributes(typeof(PK), true);
                if (attributes.Count() != 0)
                {
                    object obj = i.GetValue(t, null);
                    string name = i.Name;
                    WhereColumn.Append(string.Format(" And " + name + " = @" + name));
                    param.Add(new SqlParameter('@' + name, obj));
                }
            }

            SQL.Append(string.Format(@"Select top 1 * from {0} {1}", TableName, WhereColumn));
            DataSet ds = DBHelper.GetDataSet(SQL.ToString(), param.ToArray());

            T result = default(T);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                result = DataTableTo<T>(ds.Tables[0]);
            return result;
        }

        protected T GetModelbyCondition<T>(string TableName, T t)
        {
            Type type = t.GetType();
            PropertyInfo[] ps = type.GetProperties();

            StringBuilder SQL = new StringBuilder();
            StringBuilder WhereColumn = new StringBuilder();

            List<SqlParameter> param = new List<SqlParameter>();

            WhereColumn.Append(" Where 1=1 ");

            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(t, null);

                if (obj == null)
                {//去掉空属性
                    continue;
                }
                if (i.PropertyType == typeof(DateTime))
                {//时间属性初始化时未赋值会变为默认最小值
                    DateTime dt;
                    DateTime.TryParse(obj.ToString(), out dt);
                    if (dt <= DateTime.MinValue)
                        continue;
                }

                if (i.PropertyType == typeof(int))
                {
                    if (int.Parse(obj.ToString()) == 0)
                        continue;
                }

                string name = i.Name;
                WhereColumn.Append(string.Format(" And " + name + " = @" + name));
                param.Add(new SqlParameter('@' + name, obj));
            }

            SQL.Append(string.Format(@"Select top 1 * from {0} {1}", TableName, WhereColumn));
            DataSet ds = DBHelper.GetDataSet(SQL.ToString(), param.ToArray());

            T result = default(T);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                result = DataTableTo<T>(ds.Tables[0]);
            return result;
        }

        protected List<T> GetListbyCondition<T>(string TableName, T t)
        {
            Type type = t.GetType();
            PropertyInfo[] ps = type.GetProperties();

            StringBuilder SQL = new StringBuilder();
            StringBuilder WhereColumn = new StringBuilder();

            List<SqlParameter> param = new List<SqlParameter>();

            WhereColumn.Append(" Where 1=1 ");

            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(t, null);

                if (obj == null)
                {//去掉空属性
                    continue;
                }
                if (i.PropertyType == typeof(DateTime))
                {//时间属性初始化时未赋值会变为默认最小值
                    DateTime dt;
                    DateTime.TryParse(obj.ToString(), out dt);
                    if (dt <= DateTime.MinValue)
                        continue;
                }

                if (i.PropertyType == typeof(int))
                {
                    if (int.Parse(obj.ToString()) == 0)
                        continue;
                }

                string name = i.Name;
                WhereColumn.Append(string.Format(" And " + name + " = @" + name));
                param.Add(new SqlParameter('@' + name, obj));
            }

            SQL.Append(string.Format(@"Select * from {0} {1}", TableName, WhereColumn));
            DataSet ds = DBHelper.GetDataSet(SQL.ToString(), param.ToArray());

            List<T> result = new List<T>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                result = DataTableToList<T>(ds.Tables[0]).ToList();
            return result;
        }

        protected static T DataTableTo<T>(DataTable table)
        {
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            DataRow row = table.Rows[0];
            t = Activator.CreateInstance<T>();
            propertypes = t.GetType().GetProperties();
            foreach (PropertyInfo pro in propertypes)
            {
                tempName = pro.Name;
                if (table.Columns.Contains(tempName))
                {
                    object value = (row[tempName]) == null ? DBNull.Value : row[tempName];
                    pro.SetValue(t, value, null);
                }
            }

            return t;
        }

        protected static IList<T> DataTableToList<T>(DataTable table)
        {
            IList<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = ((row[tempName]) == DBNull.Value) ? null: row[tempName];
                        pro.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }

        protected int UpdateModelbyPK<T>(string TableName, T t)
        {
            Type type = t.GetType();
            PropertyInfo[] ps = type.GetProperties();

            StringBuilder SQL = new StringBuilder();
            StringBuilder WhereColumn = new StringBuilder();
            StringBuilder UpdateCondition = new StringBuilder();

            List<SqlParameter> param = new List<SqlParameter>();

            WhereColumn.Append(" Where 1=1 ");


            for (int j = 0; j < ps.Count(); j++)
            {
                PropertyInfo i = ps[j];
                object[] attributes = i.GetCustomAttributes(typeof(PK), true);
                object obj = i.GetValue(t, null);
                if (obj == null)
                {//去掉空属性
                    continue;

                }
                if (i.PropertyType == typeof(DateTime))
                {//时间属性初始化时未赋值会变为默认最小值
                    DateTime dt;
                    DateTime.TryParse(obj.ToString(), out dt);
                    if (dt <= DateTime.MinValue)
                        continue;
                }

                if (i.PropertyType == typeof(int))
                {
                    if (int.Parse(obj.ToString()) == 0)
                        continue;
                }
                string name = i.Name;
                if (attributes.Count() != 0)
                {
                    WhereColumn.Append(string.Format(" And " + name + " = @" + name));
                    param.Add(new SqlParameter('@' + name, obj));
                }
                else
                {
                    UpdateCondition.Append(string.Format(name + " = @" + name + ","));
                    param.Add(new SqlParameter('@' + name, obj));
                }
            }
            UpdateCondition.Remove(UpdateCondition.Length - 1, 1);
            SQL.Append(string.Format(@"Update {0} SET {1} {2}", TableName, UpdateCondition, WhereColumn));
            int result = DBHelper.ExcuteScalarSQL(SQL.ToString(), param.ToArray());
            return result;
        }

        protected int UpdateModelbyCondition<T>(string TableName, T t, T condition)
        {
            Type type = condition.GetType();
            PropertyInfo[] ps = type.GetProperties();
            StringBuilder SQL = new StringBuilder();
            StringBuilder WhereColumn = new StringBuilder();
            StringBuilder UpdateCondition = new StringBuilder();

            List<SqlParameter> param = new List<SqlParameter>();

            WhereColumn.Append(" Where 1=1 ");

            for (int j = 0; j < ps.Count(); j++)
            {
                PropertyInfo i = ps[j];

                object obj = i.GetValue(condition, null);
                if (obj == null)
                {//去掉空属性
                    continue;
                }
                if (i.PropertyType == typeof(DateTime))
                {//时间属性初始化时未赋值会变为默认最小值
                    DateTime dt;
                    DateTime.TryParse(obj.ToString(), out dt);
                    if (dt <= DateTime.MinValue)
                        continue;
                }

                if (i.PropertyType == typeof(int))
                {
                    if (int.Parse(obj.ToString()) == 0)
                        continue;
                }
                string name = i.Name;

                WhereColumn.Append(string.Format(" And " + name + " = @w" + name));
                param.Add(new SqlParameter("@w" + name, obj));
            }

    
            for (int j = 0; j < ps.Count(); j++)
            {
                PropertyInfo i = ps[j];

                object obj = i.GetValue(t, null);
                if (obj == null)
                {//去掉空属性
                    continue;
                }
                if (i.PropertyType == typeof(DateTime))
                {//时间属性初始化时未赋值会变为默认最小值
                    DateTime dt;
                    DateTime.TryParse(obj.ToString(), out dt);
                    if (dt <= DateTime.MinValue)
                        continue;
                }

                if (i.PropertyType == typeof(int))
                {
                    if (int.Parse(obj.ToString()) == 0)
                        continue;
                }
                string name = i.Name;

                UpdateCondition.Append(string.Format(name + " = @U" + name + ","));
                param.Add(new SqlParameter("@U" + name, obj));
            }

            UpdateCondition.Remove(UpdateCondition.Length - 1, 1);
            SQL.Append(string.Format(@"Update {0} SET {1} {2}", TableName, UpdateCondition, WhereColumn));
            int result = DBHelper.ExcuteScalarSQL(SQL.ToString(), param.ToArray());
            return result;
        }

    }
}
