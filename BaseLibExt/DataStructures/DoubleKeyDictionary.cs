using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLibExt.DataStructures
{
    /// <summary>
    ///     Two-level dictionary with two keys.
    /// </summary>
    /// <typeparam name="TKey1">Type of the first key.</typeparam>
    /// <typeparam name="TKey2">Type of the second key.</typeparam>
    /// <typeparam name="TValue">Type of the value stored.</typeparam>
    public class DoubleKeyDictionary<TKey1, TKey2, TValue> : IEnumerable<DoubleKeyDictionaryItem<TKey1, TKey2, TValue>>
    {
        private Dictionary<TKey1, Dictionary<TKey2, TValue>> dictionary;

        private IList<DoubleKeyDictionaryItem<TKey1, TKey2, TValue>> items;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DoubleKeyDictionary{TKey1, TKey2, TValue}" /> class.
        /// </summary>
        public DoubleKeyDictionary()
        {
            this.dictionary = new Dictionary<TKey1, Dictionary<TKey2, TValue>>();
            this.items = new List<DoubleKeyDictionaryItem<TKey1, TKey2, TValue>>();
        }

        /// <summary>
        ///     Gets the overall number of stored values.
        /// </summary>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        /// <summary>
        ///     Adds a value under a key combination.
        /// </summary>
        /// <param name="key1">The first key.</param>
        /// <param name="key2">The second key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            var secondLevel = this.GetSecondLevel(key1, true);

            secondLevel.Add(key2, value);

            this.items.Add(new DoubleKeyDictionaryItem<TKey1, TKey2, TValue>(key1, key2, value));
        }

        /// <summary>
        ///     Check if a value is stored under a key combination.
        /// </summary>
        /// <param name="key1">The first key.</param>
        /// <param name="key2">The second key.</param>
        /// <returns>True if yes.</returns>
        public bool Contains(TKey1 key1, TKey2 key2)
        {
            var value = default(TValue);
            return this.TryGetValue(key1, key2, out value);
        }

        /// <summary>
        ///     Gets all values.
        /// </summary>
        /// <returns>All values</returns>
        public IEnumerable<TValue> GetAll()
        {
            IList<TValue> values = new List<TValue>();

            foreach (var kv1 in this.dictionary)
            {
                foreach (var kv2 in kv1.Value)
                {
                    values.Add(kv2.Value);
                }
            }

            return values;
        }

        /// <summary>
        ///     Gets all values for the specified first level key.
        /// </summary>
        /// <param name="key1">The key1.</param>
        /// <returns>All values.</returns>
        public IEnumerable<TValue> GetAll(TKey1 key1)
        {
            var dict2 = this.GetSecondLevel(key1);
            return dict2.Select(x => x.Value);
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<DoubleKeyDictionaryItem<TKey1, TKey2, TValue>> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        ///     Tries the get value.
        /// </summary>
        /// <param name="key1">The key1.</param>
        /// <param name="key2">The key2.</param>
        /// <param name="value">The value.</param>
        /// <returns>True if value was found.</returns>
        public bool TryGetValue(TKey1 key1, TKey2 key2, out TValue value)
        {
            var result = false;
            value = default(TValue);

            var secondLevel = this.GetSecondLevel(key1);
            if (secondLevel != null)
            {
                result = secondLevel.TryGetValue(key2, out value);
            }

            return result;
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        private Dictionary<TKey2, TValue> GetSecondLevel(TKey1 key1, bool create = false)
        {
            Dictionary<TKey2, TValue> secondLevel = null;

            if (!this.dictionary.TryGetValue(key1, out secondLevel))
            {
                if (create)
                {
                    secondLevel = new Dictionary<TKey2, TValue>();
                    this.dictionary.Add(key1, secondLevel);
                }
            }

            return secondLevel;
        }
    }
}
