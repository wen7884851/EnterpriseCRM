using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Domain.Site.Models
{
    public class DataTableParameter: IDataTableParameter
    {
        private int _pageIndex;

        public int PageIndex
        {
            get { return _pageIndex > 0 ? _pageIndex : 1; }
            set { _pageIndex = value; }
        }

        private int _pageSize;
        public int PageSize
        {
            get
            {
                if (_pageSize <= 0)
                {
                    return 20;
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

        public string OrderBy {
            get { return _orderBy; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace(" ", "");
                    var reg = new Regex(@"^[; ]+$");
                    if (reg.IsMatch(value))
                    {
                        _orderBy = null;
                    }
                    else
                    {
                        _orderBy = value;

                    }
                }
            }
        }
        public bool Asc {get;set;}
    }
}