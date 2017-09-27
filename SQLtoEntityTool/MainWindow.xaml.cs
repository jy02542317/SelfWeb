﻿using System;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(TbxSql.Document.ContentStart, TbxSql.Document.ContentEnd);
            string sql = textRange.Text.Trim();
            if (string.IsNullOrEmpty(sql))
            {
                string result = "SQL不能为空！";
                Clear();
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
                        ConvertToEntity(ds.Tables[0]);
                }
                catch (Exception ex)
                {
                    Clear();
                    string result = ex.ToString();
                    Paragraph Pr = new Paragraph();
                    Pr.Inlines.Add(result);
                    Rtbx.Document.Blocks.Add(Pr);
                }
            }

        }

        private void ConvertToEntity(DataTable dt)
        {
            int count = dt.Columns.Count;
            string result = string.Empty;
            for (int i = 0; i < count; i++)
            {
                StringBuilder AttributeName = new StringBuilder();
                Type type = dt.Columns[i].DataType;
                string type_name = ConvertDataType(type.Name);
                string name = UpperCase(dt.Columns[i].ColumnName);
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
            Rtbx.Document.Blocks.Clear();
            Paragraph Pr = new Paragraph();
            Pr.Inlines.Add(result);
            Rtbx.Document.Blocks.Add(Pr);
        }

        private string ConvertDataType(string name)
        {
            string result = string.Empty;
            switch (name)
            {
                case "Int32":
                    result = "int";
                    break;
                case "Int16":
                    result = "short";
                    break;
                case "Int64":
                    result = "long";
                    break;
                case "String":
                    result = "string";
                    break;
                case "Boolean":
                    result = "bool";
                    break;
                case "Byte[]":
                    result = "byte[]";
                    break;
                case "Double":
                    result = "double";
                    break;
                case "Decimal":
                    result = "decimal";
                    break;
                case "Char":
                    result = "char";
                    break;
                case "Single":
                    result = "float";
                    break;
                case "Byte":
                    result = "byte";
                    break;
                case "Object":
                    result = "object";
                    break;
                case "Guid":
                    result = "Guid";
                    break;
                default:
                    result = name;
                    break;
            }
            return result;
        }

        public String UpperCase(String str)
        {
            char[] ch = str.ToCharArray();
            if (ch[0] >= 'a' && ch[0] <= 'z')
            {
                ch[0] = (char)(ch[0] - 32);
            }
            return new String(ch);
        }

        public void Clear()
        {
            TbxSql.Document.Blocks.Clear();
            Rtbx.Document.Blocks.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
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
            connection = logic.ComposeSQL(ip, db, dbus, dbpd);
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

        }
    }
}
