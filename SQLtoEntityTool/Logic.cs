using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Util;

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

    }
}
