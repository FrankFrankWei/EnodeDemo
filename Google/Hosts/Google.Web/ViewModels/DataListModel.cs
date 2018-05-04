using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Google.Web.ViewModels
{
    public class DataListModel<T>
    {
        public List<T> DataList { get; set; }

        public int PageIndex { get; set; }

        public int TotalPage { get; set; }

        public int TotalCount { get; set; }
    }
}