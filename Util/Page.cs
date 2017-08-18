using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class Page
    {
        private int _page;

        public int Pages
        {
            get { return _page; }
            set { _page = value; }
        }

        private int _rows;

        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        private int _size;

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private int _total;

        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }

        private int _allpage;

        public int Allpage
        {
            get { return _allpage; }
            set { _allpage = value; }
        }

    }
}
