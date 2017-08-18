using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAO
{
    public class DAO_Account : BaseDao<Account>
    {
        private const string TableName = "Account";
        public int insert(Account t)
        {
            return Insert<Account>(TableName, t, true);
        }

        public bool exist(Account t)
        {
            return Exist<Account>(TableName, t);
        }

        public Account get_model_by_PK(Account t)
        {
            return GetModelbyPK<Account>(TableName, t);
        }

        public Account get_model_by_condition(Account t)
        {
            return GetModelbyCondition<Account>(TableName, t);
        }

        public List<Account> get_model_list_by_condition(Account t)
        {
            return GetListbyCondition<Account>(TableName, t);
        }

        public int update_model_by_PK(Account t)
        {
            return UpdateModelbyPK<Account>(TableName, t);
        }

        public int update_model_by_Condition(Account t, Account condition)
        {
            return UpdateModelbyCondition<Account>(TableName, t, condition);
        }
    }
}
