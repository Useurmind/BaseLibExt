using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibExt.DataStructures
{
    /// <summary>
    /// Item in <see cref="DoubleKeyDictionary{TKey1,TKey2,TValue}"/>
    /// </summary>
    /// <typeparam name="TKey1">The type of the key1.</typeparam>
    /// <typeparam name="TKey2">The type of the key2.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class DoubleKeyDictionaryItem<TKey1, TKey2, TValue>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DoubleKeyDictionaryItem{TKey1, TKey2, TValue}" /> class.
        /// </summary>
        /// <param name="key1">The key1.</param>
        /// <param name="key2">The key2.</param>
        /// <param name="value">The value.</param>
        public DoubleKeyDictionaryItem(TKey1 key1, TKey2 key2, TValue value)
        {
            this.Key1 = key1;
            this.Key2 = key2;
            this.Value = value;
        }

        /// <summary>
        ///     Gets the key1.
        /// </summary>
        /// <value>
        ///     The key1.
        /// </value>
        public TKey1 Key1 { get; private set; }

        /// <summary>
        ///     Gets the key2.
        /// </summary>
        /// <value>
        ///     The key2.
        /// </value>
        public TKey2 Key2 { get; private set; }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public TValue Value { get; private set; }
    }
}
