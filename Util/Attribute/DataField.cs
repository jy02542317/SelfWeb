using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Attribute
{
    public class DataField : System.Attribute
    {
        bool hasSet = false;

        public bool HasSet
        {
            get { return hasSet; }
            set { hasSet = value; }
        }

    }
}
