using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Util;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SQLtoEntityTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(TbxSql.Document.ContentStart, TbxSql.Document.ContentEnd);
            string sql = textRange.Text.Trim();
            string result = string.Empty;
            if (string.IsNullOrEmpty(sql))
            {
                Clear();
                result = "SQL不能为空！"; 
                Paragraph Pr = new Paragraph();
                Pr.Inlines.Add(result);
                Rtbx.Document.Blocks.Add(Pr);
            }
            else
            {
                try
                {
                    DataSet ds = DBHelper.GetDataSet(sql);

                    if (ds != null)
                    {
                        Logic logic = new Logic();
                        result = logic.ConvertToEntity(ds.Tables[0]);
                    }
                        
                    Rtbx.Document.Blocks.Clear();
                    Paragraph Pr = new Paragraph();
                    Pr.Inlines.Add(result);
                    Rtbx.Document.Blocks.Add(Pr);
                }
                catch (Exception ex)
                {
                    Clear();
                    result = ex.ToString();
                    Paragraph Pr = new Paragraph();
                    Pr.Inlines.Add(result);
                    Rtbx.Document.Blocks.Add(Pr);
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connection = string.Empty;
            getCondition(out connection);
            if (string.IsNullOrEmpty(connection))
            {
                return;
            }
            Logic logic = new Logic();
            bool result = logic.ConnectSQLTest(connection);
            if (result)
            {
                MessageBox.Show("连接成功");
            }
            else
            {
                MessageBox.Show("连接失败");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string connection = string.Empty;
            getCondition(out connection);
            if (string.IsNullOrEmpty(connection))
            {
                return;
            }
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettings mySettings = new ConnectionStringSettings("connectionString", connection, "System.Data.SqlClient ");
            if (ConfigurationManager.ConnectionStrings["connectionString"] != null)
                config.ConnectionStrings.ConnectionStrings.Remove("connectionString");
            // 添加新的连接字符串
            config.ConnectionStrings.ConnectionStrings.Add(mySettings);
            // 保存对配置文件的更改
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("connectionStrings");
            DBHelper.doRefresh();//静态变量不刷新的话起不到刷新的作用
        }
        #endregion

        #region pageMethod
        private void getCondition(out string result)
        {
            result = string.Empty;
            string ip = this.TBXIP.Text;
            string db = this.TBXDB.Text;
            string dbus = this.TBXDBUS.Text;
            string dbpd = this.TBXDBPD.Text;

            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("请填写IP");
                return;
            }

            if (string.IsNullOrEmpty(db))
            {
                MessageBox.Show("请填写数据库名称");
                return;
            }

            if (string.IsNullOrEmpty(dbus))
            {
                MessageBox.Show("请填写数据库用户名");
                return;
            }

            if (string.IsNullOrEmpty(dbpd))
            {
                MessageBox.Show("请填写数据库密码");
                return;
            }

            string connection = string.Empty;
            Logic logic = new Logic();
            result = logic.ComposeSQL(ip, db, dbus, dbpd);
        }

        public void Clear()
        {
            TbxSql.Document.Blocks.Clear();
            Rtbx.Document.Blocks.Clear();
        }
        #endregion
    }
}
