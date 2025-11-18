using System;
using System.Linq;
using System.Linq.Expressions;

namespace Sudoku.Application.Helpers;

public static class LinqExtensions
{
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
    {
        if (condition)
            return source.Where(predicate);

        return source;
    }
    public static IQueryable<TSource> WhereIfNotNull<TSource, TData>(this IQueryable<TSource> source, TData data, Expression<Func<TSource, bool>> predicate)
    {
        if (data != null || (typeof(TData) == typeof(string) && !string.IsNullOrEmpty(data as string)))
            return source.Where(predicate);

        return source;
    }
}
