using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jwc.Experiment
{
    public class CompositeEnumerableTest
    {
        [Fact]
        public void SutIsEnumerable()
        {
            var sut = new CompositeEnumerable<object>();
            Assert.IsAssignableFrom<IEnumerable<object>>(sut);
        }

        [Fact]
        public void InitializeWithNullItemSetThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new CompositeEnumerable<object>(null));
        }

        [Fact]
        public void SutEnumeratesCorrectItems()
        {
            var item1 = 234;
            var item2 = 12;
            var item3 = 865;
            var item4 = 24;
            var item5 = 58;
            IEnumerable<int>[] itemSet =
            {
                new[] { item1 },
                new[] { item2, item3 },
                new[] { item4, item5 }
            };
            var sut = new CompositeEnumerable<int>(itemSet);

            var actual = sut.ToArray();

            Assert.Equal(item1, actual[0]);
            Assert.Equal(item2, actual[1]);
            Assert.Equal(item3, actual[2]);
            Assert.Equal(item4, actual[3]);
            Assert.Equal(item5, actual[4]);
        }

        [Fact]
        public void ItemSetIsCorrect()
        {
            IEnumerable<string>[] testCaseSet =
            {
                new[] { "aaa" },
                new[] { "bbb", "ccc" }
            };
            var sut = new CompositeEnumerable<string>(testCaseSet);

            var actual = sut.ItemSet;

            Assert.Equal(testCaseSet, actual);
        }
    }
}