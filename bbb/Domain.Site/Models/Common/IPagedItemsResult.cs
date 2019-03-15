using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public interface IPagedItemsResult<T>
    {
        IEnumerable<T> Items { get; set; }
        int TotalItemsCount { get; set; }
    }
}
