using System.Linq.Expressions;
using System.Reflection;

namespace Productos.API.Utils
{
    public static class Ordering
    {
        public static IOrderedQueryable<TSource> ApplyOrderDirection<TSource>(this IQueryable<TSource> source, string attributeName, bool sortOrder)
        {
            if (String.IsNullOrEmpty(attributeName))
            {
                return source as IOrderedQueryable<TSource>;
            }

            var propertyInfo = typeof(TSource).GetProperty(attributeName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new ArgumentException("ApplyOrderDirection: The associated Attribute to the given AttributeName could not be resolved", attributeName);
            }

            var parameter = Expression.Parameter(typeof(TSource));
            var property = Expression.Property(parameter, attributeName);
            var propAsObject = Expression.Convert(property, typeof(object));

            Expression<Func<TSource, object>> expression = Expression.Lambda<Func<TSource, object>>(propAsObject, parameter);

            if (sortOrder)
            {
                return source.OrderBy(expression);
            }
            else
            {
                return source.OrderByDescending(expression);
            }
        }
    }
}
