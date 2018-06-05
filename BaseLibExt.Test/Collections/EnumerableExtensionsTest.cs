using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using BaseLibExt.Collections;
using BaseLibExt.Utils;

using FluentAssertions;

using Xunit;

namespace BaseLibExt.Test.Collections
{
    /// <summary>
    ///     Tests for <see cref="Utils.EnumerableExtensions" />
    /// </summary>
    public class EnumerableExtensionsTest
    {
        /// <summary>
        ///     Checks that the DistinctBy function works.
        /// </summary>
        [Fact]
        public void DistinctByWorks()
        {
            var list = new List<TestObject>()
            {
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 1
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 2
                },
                new TestObject()
                {
                    StringProperty = "b",
                    IntProperty = 3
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 4
                },
                new TestObject()
                {
                    StringProperty = "b",
                    IntProperty = 5
                },
                new TestObject()
                {
                    StringProperty = "c",
                    IntProperty = 6
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 7
                }
            };

            var distinctList = list.DistinctBy(x => x.StringProperty);

            distinctList.Count().Should().Be(3);
            distinctList.ElementAt(0).IntProperty.Should().Be(1);
            distinctList.ElementAt(1).IntProperty.Should().Be(3);
            distinctList.ElementAt(2).IntProperty.Should().Be(6);
        }

        /// <summary>
        ///     First test.
        /// </summary>
        [Fact]
        public void GroupByWorks()
        {
            Expression<Func<TestObject, string>> strongKeySelector = o => o.StringProperty;
            var strongList = new List<TestObject>()
            {
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 1
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 2
                },
                new TestObject()
                {
                    StringProperty = "b",
                    IntProperty = 3
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 4
                },
                new TestObject()
                {
                    StringProperty = "b",
                    IntProperty = 5
                },
                new TestObject()
                {
                    StringProperty = "c",
                    IntProperty = 6
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 7
                }
            };

            var genericList = (IEnumerable)strongList;
            var genericKeySelector = (LambdaExpression)strongKeySelector;

            var groups = genericList.GroupBy(genericKeySelector);

            groups.Count().Should().Be(3);
            groups.ElementAt(0).Key.Should().Be("a");
            groups.ElementAt(1).Key.Should().Be("b");
            groups.ElementAt(2).Key.Should().Be("c");

            groups.ElementAt(0).Should().BeEquivalentTo(strongList.Where(x => x.StringProperty == "a"));
            groups.ElementAt(1).Should().BeEquivalentTo(strongList.Where(x => x.StringProperty == "b"));
            groups.ElementAt(2).Should().BeEquivalentTo(strongList.Where(x => x.StringProperty == "c"));
        }

        /// <summary>
        /// Checks that IndexOf works.
        /// </summary>
        /// <param name="intPropertyValue">The int property value.</param>
        /// <param name="expectedIndex">The expected index.</param>
        [Theory]
        [InlineData(1, 0)]
        [InlineData(5, 4)]
        [InlineData(10, -1)]
        public void IndexOfWorks(int intPropertyValue, int expectedIndex)
        {
            var list = new List<TestObject>()
            {
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 1
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 2
                },
                new TestObject()
                {
                    StringProperty = "b",
                    IntProperty = 3
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 4
                },
                new TestObject()
                {
                    StringProperty = "b",
                    IntProperty = 5
                },
                new TestObject()
                {
                    StringProperty = "c",
                    IntProperty = 6
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 7
                }
            };

            var result = list.IndexOf(x => x.IntProperty == intPropertyValue);

            result.Should().Be(expectedIndex);
        }

        /// <summary>
        ///     Checks that MaxElement and MinElement function works.
        /// </summary>
        [Fact]
        public void MaxMinElementWorks()
        {
            var list = new List<TestObject>()
            {
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 1
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = 3
                },
                new TestObject()
                {
                    StringProperty = "a",
                    IntProperty = -1
                },
                new TestObject()
                {
                    StringProperty = "b",
                    IntProperty = 2
                }
            };

            list.MaxElement(x => x.IntProperty).Should().Be(list[1]);
            list.MinElement(x => x.IntProperty).Should().Be(list[2]);
        }

        private class TestObject
        {
            public int IntProperty { get; set; }

            public string StringProperty { get; set; }
        }
    }
}
