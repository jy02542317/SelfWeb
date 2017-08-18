using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Menu : BaseModel
    {
        

        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _url;
        public string url
        {
            get { return _url; }
            set { _url = value; }
        }

        private int _parent;
        public int parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        private DateTime _create_time;
        public DateTime create_time
        {
            get { return _create_time; }
            set { _create_time = value; }
        }

        private int _create_account;
        public int create_account
        {
            get { return _create_account; }
            set { _create_account = value; }
        }

        private DateTime _modify_time;
        public DateTime modify_time
        {
            get { return _modify_time; }
            set { _modify_time = value; }
        }

        private int _modify_account;
        public int modify_account
        {
            get { return _modify_account; }
            set { _modify_account = value; }
        }

        private bool _is_valid;
        public bool is_valid
        {
            get { return _is_valid; }
            set { _is_valid = value; }
        }

    }
}
