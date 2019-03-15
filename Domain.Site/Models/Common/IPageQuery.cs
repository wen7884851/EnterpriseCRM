﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public interface IPageQuery
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        string OrderBy { get; set; }
    }
}
