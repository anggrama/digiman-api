using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace digiman_dal.Extensions
{
    public static class OrderExtensions
    {

        public static IQueryable<T> Order<T, TKey>(this IQueryable<T> source, Func<T, TKey> selector, bool ascending)
        {
            if (ascending)
                return source.OrderBy(selector).AsQueryable();
            else
                return source.OrderByDescending(selector).AsQueryable();
        }

       public static IEnumerable<T> Order<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, bool ascending)
        {
            if (ascending)
                return source.OrderBy(selector);
            else
                return source.OrderByDescending(selector);
        }
    }
}
