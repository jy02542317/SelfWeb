using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLtoEntityTool
{
    public static class Util
    {
        public static String UpperCase(String str)
        {
            char[] ch = str.ToCharArray();
            if (ch[0] >= 'a' && ch[0] <= 'z')
            {
                ch[0] = (char)(ch[0] - 32);
            }
            return new String(ch);
        }

        public static string ConvertDataType(string name)
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
    }
}
