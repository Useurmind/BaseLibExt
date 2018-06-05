using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BaseLibExt.Collections
{
    /// <summary>
    /// Extension methods for the enumerable class.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Groups the enumerable by the key specified in the key selector.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="keySelector">The key selector, a lambda looking like this x => x.KeyProperty .</param>
        /// <returns>The grouped enumerable.</returns>
        public static IEnumerable<IGrouping<object, object>> GroupBy(this IEnumerable enumerable, LambdaExpression keySelector)
        {
            var groupings = new Dictionary<object, Grouping<object, object>>();

            var compiledKeySelector = keySelector.Compile();

            foreach (var item in enumerable)
            {
                var key = compiledKeySelector.DynamicInvoke(item);
                Grouping<object, object> grouping = null;
                if (!groupings.TryGetValue(key, out grouping))
                {
                    grouping = new Grouping<object, object>(key);
                    groupings.Add(key, grouping);
                }

                grouping.Items.Add(item);
            }

            return groupings.Values;
        }

        /// <summary>
        /// Get elements distinct by a certain key property selected via the lambda.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>The distinct elements by the key selector.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector, EqualityComparer<TKey>.Default);
        }

        /// <summary>
        /// Gets the index of an element fullfilling a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The index of the element or -1 if not found.</returns>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var index = 0;
            foreach (var element in source)
            {
                if (predicate(element))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Gets the element with the maximum value in the given property.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <returns>
        /// The element with the largest value in the property.
        /// </returns>
        public static TSource MaxElement<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> propertySelector)
            where TSource : class
        {
            return source.NonNull().OrderByDescending(propertySelector).FirstOrDefault();
        }

        /// <summary>
        /// Gets the element with the minimum value in the given property.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <returns>
        /// The element with the smallest value in the property.
        /// </returns>
        public static TSource MinElement<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> propertySelector)
            where TSource : class
        {
            return source.NonNull().OrderBy(propertySelector).FirstOrDefault();
        }

        /// <summary>
        /// Filters out elements that are null.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>Enumerable without nulls.</returns>
        public static IEnumerable<TSource> NonNull<TSource>(this IEnumerable<TSource> source)
            where TSource : class
        {
            return source.Where(x => x != null);
        }

        /// <summary>
        /// Joins the given list of strings.
        /// Forwards straight to the string.Join method.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="separator">The separator to joins.</param>
        /// <returns>The string joined from the list of strings.</returns>
        public static string JoinStrings(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        private static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            return DistinctByImpl(source, keySelector, comparer);
        }

        private static IEnumerable<TSource> DistinctByImpl<TSource, TKey>(
            IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>(comparer);
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
