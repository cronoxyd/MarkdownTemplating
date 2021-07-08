using System;
using System.Linq;
using System.Collections.Generic;

namespace MarkdownTemplating.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, dynamic predicate)
        {
            return source.All(item => Convert.ToBoolean(predicate(item)));
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, dynamic selector)
        {
            return source.Select<TSource, TResult>(item => selector(item));
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, dynamic predicate)
        {
            return source.Where(item => Convert.ToBoolean(predicate(item)));
        }
    }
}