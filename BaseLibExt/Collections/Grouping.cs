using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BaseLibExt.Collections
{
    /// <summary>
    ///     Simple implementation of the grouping class.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <seealso cref="System.Linq.IGrouping{TKey, TItem}" />
    public class Grouping<TKey, TItem> : IGrouping<TKey, TItem>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Grouping{TKey, TItem}" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public Grouping(TKey key)
        {
            this.Key = key;
            this.Items = new List<TItem>();
        }

        /// <summary>
        ///     Gets the items of the grouping.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        public List<TItem> Items { get; }

        /// <summary>
        ///     Gets the key of the <see cref="T:System.Linq.IGrouping`2" />.
        /// </summary>
        public TKey Key { get; }

        /// <inheritdoc />
        public IEnumerator<TItem> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
