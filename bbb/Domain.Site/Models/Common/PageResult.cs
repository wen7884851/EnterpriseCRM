using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Site.Models
{
    public class PageResult<T>:IPagedItemsResult<T>
    {
        [DataMember]
        public IEnumerable<T> Items { get; set; } = new List<T>();
        [DataMember]
        public int TotalItemsCount { get; set; }
    }
}
