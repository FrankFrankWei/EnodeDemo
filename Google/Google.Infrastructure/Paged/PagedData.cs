using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.Infrastructure.Paged
{
    public class PagedData<T> : List<T>, IPagedData<T>
    {
        public PagedData(List<T> items, int pageIndex, int pageSize, int totalItem)
        {
            AddRange(items);
            PageSize = pageSize;
            TotalCount = totalItem;
            TotalPage = (int)Math.Ceiling(totalItem / (double)pageSize);
            if (TotalPage < PageIndex)
            {
                PageIndex = TotalPage;
            }
            else
            {
                PageIndex = pageIndex;
            }
        }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPage { get; private set; }
    }

    public interface IPagedData<T> : IEnumerable<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalPage { get; }
        int TotalCount { get; }
    }

    public static class PageDataExtension
    {
        public static PagedData<T> ToPagedData<T>(this List<T> list, int pageIndex, int pageSize, int totalItem)
        {
            return new PagedData<T>(list, pageIndex, pageSize, totalItem);
        }
    }
}
