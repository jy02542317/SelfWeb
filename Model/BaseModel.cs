using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BaseModel
    {
        private string _changed_column;

        protected string changed_column
        {
            get { return _changed_column; }
            set { _changed_column = value; }
        }

    }
}
