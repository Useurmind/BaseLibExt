using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseLibExt.DataStructures;

using Xunit;
using FluentAssertions;

namespace BaseLibExt.Test.DataStructures
{
    /// <summary>
    ///     Tests for <see cref="DoubleKeyDictionary{TKey1,TKey2,TValue}" />
    /// </summary>
    public class DoubleKeyDictionaryTest
    {
        private DoubleKeyDictionary<int, string, int> dictionary;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DoubleKeyDictionaryTest" /> class.
        /// </summary>
        public DoubleKeyDictionaryTest()
        {
            this.dictionary = new DoubleKeyDictionary<int, string, int>();
        }

        /// <summary>
        ///     Adds the empty string key works.
        /// </summary>
        [Fact]
        public void Add_EmptyStringKey_Works()
        {
            var key1 = 2;
            var key2 = string.Empty;
            var value = 6;

            this.dictionary.Add(key1, key2, value);

            var resultValue = 0;
            var found = false;
            found = this.dictionary.TryGetValue(key1, key2, out resultValue);

            Assert.True(found);
            Assert.Equal(value, resultValue);
        }

        /// <summary>
        ///     Adds some combination try get value returns combination.
        /// </summary>
        [Fact]
        public void Add_SomeCombination_TryGetValueReturnsCombination()
        {
            var key1 = 2;
            var key2 = "sdf";
            var value = 6;

            var resultValue = 0;
            var found = false;

            this.dictionary.Add(key1, key2, value);
            found = this.dictionary.TryGetValue(key1, key2, out resultValue);

            Assert.True(found);
            Assert.Equal(value, resultValue);
        }

        /// <summary>
        ///     Addeds the item is found.
        /// </summary>
        [Fact]
        public void AddedItemIsFound()
        {
            var key1 = 5;
            var key2 = "key2";
            var value = "asmdpsodfgkhöglhmdfgh d";

            var dictionary = new DoubleKeyDictionary<int, string, string>();

            dictionary.Add(key1, key2, value);

            string resultValue = null;
            var result = dictionary.TryGetValue(key1, key2, out resultValue);

            result.Should().BeTrue();
            resultValue.Should().BeSameAs(value);
        }

        /// <summary>
        ///     Addeds the items keys are found.
        /// </summary>
        [Fact]
        public void AddedItemsKeysAreFound()
        {
            var key1 = 5;
            var key2 = "key2";
            var value = "asmdpsodfgkhöglhmdfgh d";

            var dictionary = new DoubleKeyDictionary<int, string, string>();

            dictionary.Add(key1, key2, value);

            var result = dictionary.Contains(key1, key2);

            result.Should().BeTrue();
        }

        /// <summary>
        ///     Addings the item twice throws.
        /// </summary>
        [Fact]
        public void AddingItemTwiceThrows()
        {
            var key1 = 5;
            var key2 = "key2";
            var value = "asmdpsodfgkhöglhmdfgh d";

            var dictionary = new DoubleKeyDictionary<int, string, string>();

            dictionary.Add(key1, key2, value);

            Action addSecondTime = () => { dictionary.Add(key1, key2, value); };

            addSecondTime.Should().Throw<Exception>();
        }

        /// <summary>
        ///     Foreaches some elements all elements found.
        /// </summary>
        [Fact]
        public void Foreach_SomeElements_AllElementsFound()
        {
            var key11 = 1;
            var key21 = "dsg";
            var value1 = 34;

            var key12 = 45;
            var key22 = "sdf";
            var value2 = 98;

            this.dictionary.Add(key11, key21, value1);
            this.dictionary.Add(key12, key22, value2);

            foreach (var dictItem in this.dictionary)
            {
                Assert.True(
                    (dictItem.Key1 == key11 && dictItem.Key2 == key21 && dictItem.Value == value1)
                    || (dictItem.Key1 == key12 && dictItem.Key2 == key22 && dictItem.Value == value2));
            }
        }

        /// <summary>
        ///     Gets all some elements returns all elements.
        /// </summary>
        [Fact]
        public void GetAll_SomeElements_ReturnsAllElements()
        {
            var value1 = 45;
            var value2 = 56;
            var value3 = 87;

            this.dictionary.Add(34, "skdlfj", value1);
            this.dictionary.Add(34, "sdfg", value2);
            this.dictionary.Add(56, "hfgh", value3);

            var values = this.dictionary.GetAll();

            Assert.Equal(3, values.Count());
            Assert.True(values.Contains(value1));
            Assert.True(values.Contains(value2));
            Assert.True(values.Contains(value3));
        }

        /// <summary>
        ///     Gets all some elements returns all elements.
        /// </summary>
        [Fact]
        public void GetAllDict_SomeElements_ReturnsAllElements()
        {
            var value1 = 45;
            var value2 = 56;
            var value3 = 87;

            var key1 = "skdlfj";
            var key2 = "sdfg";

            this.dictionary.Add(34, key1, value1);
            this.dictionary.Add(34, key2, value2);
            this.dictionary.Add(56, "hfgh", value3);

            var dict = this.dictionary.GetAllDict(34);

            Assert.Equal(2, dict.Count());
            Assert.True(dict.ContainsKey(key1));
            Assert.True(dict.ContainsKey(key2));
            Assert.Equal(value1, dict[key1]);
            Assert.Equal(value2, dict[key2]);
        }

        /// <summary>
        ///     Gets all some elements returns all elements.
        /// </summary>
        [Fact]
        public void GetAllDict_UnknownKey_ReturnsEmptyDict()
        {
            var value1 = 45;
            var value2 = 56;
            var value3 = 87;

            var key1 = "skdlfj";
            var key2 = "sdfg";

            this.dictionary.Add(34, key1, value1);
            this.dictionary.Add(34, key2, value2);
            this.dictionary.Add(56, "hfgh", value3);

            var dict = this.dictionary.GetAllDict(37);

            Assert.NotNull(dict);
            Assert.Equal(0, dict.Count);
        }

        /// <summary>
        ///     Missings the item is not found.
        /// </summary>
        [Fact]
        public void MissingItemIsNotFound()
        {
            var key1 = 5;
            var key2 = "key2";

            var dictionary = new DoubleKeyDictionary<int, string, string>();

            string resultValue = null;
            var result = dictionary.TryGetValue(key1, key2, out resultValue);

            result.Should().BeFalse();
            resultValue.Should().BeNull();
        }

        /// <summary>
        ///     Missings the items keys are not found.
        /// </summary>
        [Fact]
        public void MissingItemsKeysAreNotFound()
        {
            var key1 = 5;
            var key2 = "key2";

            var dictionary = new DoubleKeyDictionary<int, string, string>();

            var result = dictionary.Contains(key1, key2);

            result.Should().BeFalse();
        }

        /// <summary>
        ///     Can remove complete first  level.
        /// </summary>
        [Fact]
        public void CanRemoveCompleteFirstLevel()
        {
            var key1 = 5;
            var key21 = "key1";
            var key22 = "key2";
            var key12 = 2;

            var dictionary = new DoubleKeyDictionary<int, string, string>();

            dictionary.Add(key1, key21, "value11");
            dictionary.Add(key1, key22, "value2");
            dictionary.Add(key12, key21, "value3");
            dictionary.Add(key12, key22, "value4");

            var result = dictionary.Remove(key1);

            result.Should().BeTrue();
            dictionary.Count.Should().Be(2);
            dictionary.Contains(key1, key21).Should().BeFalse();
            dictionary.Contains(key1, key22).Should().BeFalse();
            dictionary.GetAll().FirstOrDefault(x => x == "value11").Should().BeNullOrEmpty();
            dictionary.GetAll().FirstOrDefault(x => x == "value2").Should().BeNullOrEmpty();
        }

        /// <summary>
        ///     Can remove exact entry.
        /// </summary>
        [Fact]
        public void CanRemoveExactEntry()
        {
            var key1 = 5;
            var key21 = "key1";
            var key22 = "key2";
            var key12 = 2;

            var dictionary = new DoubleKeyDictionary<int, string, string>();

            dictionary.Add(key1, key21, "value11");
            dictionary.Add(key1, key22, "value2");
            dictionary.Add(key12, key21, "value3");
            dictionary.Add(key12, key22, "value4");

            var result = dictionary.Remove(key1, key21);

            result.Should().BeTrue();
            dictionary.Count.Should().Be(3);
            dictionary.Contains(key1, key21).Should().BeFalse();
            dictionary.GetAll().FirstOrDefault(x => x == "value11").Should().BeNullOrEmpty();
        }
        
        /// <summary>
        ///     If first level was not found TRyRemove returns false.
        /// </summary>
        [Fact]
        public void RemoveLevel1ReturnsFalseIfNothingFound()
        {
            var key1 = 5;
            var key21 = "key1";
            var key22 = "key2";
            var key12 = 2;

            var dictionary = new DoubleKeyDictionary<int, string, string>();

            dictionary.Add(key1, key21, "value11");
            dictionary.Add(key1, key22, "value2");
            dictionary.Add(key12, key21, "value3");
            dictionary.Add(key12, key22, "value4");

            var result = dictionary.Remove(3);

            result.Should().BeFalse();
        }

        /// <summary>
        ///     If entry not found TryRemove returns false.
        /// </summary>
        [Fact]
        public void RemoveLevel2ReturnsFalseIfNothingFound()
        {
            var key1 = 5;
            var key21 = "key1";
            var key22 = "key2";
            var key12 = 2;

            var dictionary = new DoubleKeyDictionary<int, string, string>();

            dictionary.Add(key1, key21, "value11");
            dictionary.Add(key1, key22, "value2");
            dictionary.Add(key12, key21, "value3");
            dictionary.Add(key12, key22, "value4");

            var result = dictionary.Remove(3, key21);
            var result2 = dictionary.Remove(5, "key3");

            result.Should().BeFalse();
            result2.Should().BeFalse();
        }
    }
}
