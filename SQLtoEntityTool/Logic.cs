using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Util;
using System.Data;

namespace SQLtoEntityTool
{
    public class Logic
    {
        public bool ConnectSQLTest(string connection)
        {
            SqlConnection cmd = new SqlConnection(connection);
            try
            {
                cmd.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                cmd.Close();
            }
        }

        public string ComposeSQL(string ip, string db,string dbus, string dbpd)
        {
            return string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3}",ip,db,dbus,dbpd);;
        }

        public string ConvertToEntity(DataTable dt)
        {
            int count = dt.Columns.Count;
            string result = string.Empty;

            for (int i = 0; i < count; i++)
            {
                StringBuilder AttributeName = new StringBuilder();
                Type type = dt.Columns[i].DataType;
                string type_name = Util.ConvertDataType(type.Name);
                string name = Util.UpperCase(dt.Columns[i].ColumnName);
                string firstFlag = type_name.ElementAt(0).ToString().ToLower();
                string pname = firstFlag + name;
                AttributeName.Append("private " + type_name + " " + pname + ";\n\n");
                AttributeName.Append("public " + type_name + " " + name + "\n");
                AttributeName.Append("{\n");
                AttributeName.Append("\t get { return " + pname + "; }\n");
                AttributeName.Append("\t set { " + pname + " = value; }\n");
                AttributeName.Append("}\n\n");
                result += AttributeName.ToString();
            }
            return result;
        }
    }
}
