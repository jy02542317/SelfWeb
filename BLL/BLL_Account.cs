using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Model;

namespace BLL
{
    public class BLL_Account
    {
        private DAO_Account dao = new DAO_Account();

        public int Add(Account t)
        {
            return dao.insert(t);
        }

        public bool Exist(Account t)
        {
            return dao.exist(t);
        }

        public Account GetModelbyId(int id)
        {
            Account account = new Account();
            account.id = id;
            return dao.get_model_by_PK(account);
        }

        public Account GetModelbyCondition(Account account)
        {
            return dao.get_model_by_condition(account);
        }

        public int UpdateModelbyPK(Account account)
        {
            return dao.update_model_by_PK(account);
        }

        public List<Account> GetModelListbyCondition(Account account)
        {
            return dao.get_model_list_by_condition(account);
        }

        public int UpdateModelbyCondition(Account account, Account condition)
        {
            return dao.update_model_by_Condition(account, condition);
        }

        public Boolean CheckPassword(Account t,string oldpassword, string newpassword, string confirmpassword, out string message)
        {

            message = string.Empty;

            if (string.IsNullOrEmpty(oldpassword.Trim()))
            {
                message = "请正确输入旧密码！";
                return false;
            }

            if (string.IsNullOrEmpty(newpassword.Trim()))
            {
                message = "请正确输入密码！";
                return false;
            }

            if (string.IsNullOrEmpty(confirmpassword.Trim()))
            {
                message = "请正确输入确认密码！";
                return false;
            }

            if(t.password!=oldpassword)
            {
                message = "旧密码不正确！";
                return false;
            }

            if (oldpassword == newpassword)
            {
                message = "新旧密码相同！";
                return false;
            }

            if (newpassword != confirmpassword)
            {
                message = "两次密码输入不一致！";
                return false;
            }

            t.password = newpassword;
            UpdateModelbyPK(t);
            message = "修改成功！";
            return true;
        }
    }
}
