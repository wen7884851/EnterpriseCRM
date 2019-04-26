using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public class PageQuery:IPageQuery
    {
        private int _page;
        public int PageIndex
        {
            get { return _page > 0 ? _page : 1; }
            set { _page = value; }
        }

        private int _pageSize;

        public int PageSize
        {
            get
            {
                if (_pageSize <= 0)
                {
                    return 10;
                }

                if (_pageSize > 100)
                {
                    return 100;
                }

                return _pageSize;
            }
            set { _pageSize = value; }
        }

        private string _orderBy;
        public string OrderBy
        {
            get
            {
                if(string.IsNullOrEmpty(_orderBy))
                {
                    return "CreateTime";
                }
                return _orderBy;
            }
            set { _orderBy = value; }
        }
    }
}
