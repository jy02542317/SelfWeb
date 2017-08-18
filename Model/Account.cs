using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Attribute;

namespace Model
{
    public class Account : BaseModel
    {

        private int _id;

        [PK]
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _username;

        public string username
        {
            get { return _username; }
            set
            {
                _username = value;
            }
        }

        private string _password;

        public string password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }

        private int _account_type;

        public int account_type
        {
            get { return _account_type; }
            set
            {
                _account_type = value;
            }
        }

    }
}
