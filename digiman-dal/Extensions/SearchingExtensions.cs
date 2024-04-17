using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NinjaNye.SearchExtensions;

namespace digiman_dal.Extensions
{
    public static class SearchingExtensions
    {
        //used by LINQ to SQL
        public static IQueryable<TSource> SearchAll<TSource>(this IQueryable<TSource> source, string search)
        {
            return source.Search().ContainingAll(search);
        }

        //used by LINQ
        public static IEnumerable<TSource> SearchAll<TSource>(this IEnumerable<TSource> source, string search)
        {
            return source.Search().ContainingAll(search);
        }
    }
}
