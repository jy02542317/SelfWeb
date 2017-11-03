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

        public string ComposeSQL(string ip, string db, string dbus, string dbpd)
        {
            return string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3}", ip, db, dbus, dbpd);
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

        public string ConvertToM2GEntity(DataTable dt, string className)
        {
            int count = dt.Columns.Count;
            string result = string.Empty;
            string resultClass = string.Empty;
            string pageResult = string.Empty;
            string allAttr = string.Empty;

            resultClass += "public " + className + "()\n";
            resultClass += "{\n";
            resultClass += "\tthis.TableName = \"[" + className + "]\";\n";

            for (int i = 0; i < count; i++)
            {
                StringBuilder AttributeName = new StringBuilder();
                StringBuilder AttributeNameClass = new StringBuilder();
                StringBuilder PageAttr = new StringBuilder();

                Type type = dt.Columns[i].DataType;
                string type_name = Util.ConvertM2gDataType(type.Name);
                string name = Util.UpperCase(dt.Columns[i].ColumnName);

                if (name == "PKID")
                {
                    AttributeName.Append("[PrimaryKey(PrimaryKeyPolicy.Auto)]\n");
                }

                AttributeName.Append("public " + type_name + " " + name + ";\n");
                AttributeNameClass.Append("\tthis."+name+" =new "+type_name+"(\"["+name+"]\", \"\");\n");
                PageAttr.Append("<asp:BoundColumn DataField=\""+name+"\" HeaderText=\"\"></asp:BoundColumn>\n");
                result += AttributeName.ToString();
                resultClass += AttributeNameClass.ToString();
                pageResult += PageAttr.ToString();
                allAttr += name+',';
            }

            resultClass += "}\n\n";
            resultClass += " public override BusinessObject Clone()\n{\n";
            resultClass += "\treturn new " + className + "();\n}\n";
            result = resultClass + result + pageResult +"\n"+ allAttr;
            return result;
        }
    }
}
