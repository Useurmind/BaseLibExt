using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseLibExt.Files;

using FluentAssertions;

using Xunit;

namespace BaseLibExt.Test.Files
{
    /// <summary>
    ///     Tests for <see cref="InMemoryFileFactory" />
    /// </summary>
    public class InMemoryFileFactoryTest
    {
        /// <summary>
        ///     Check that even a stream whose position is at the end is correctly converted.
        /// </summary>
        [Fact]
        public void WrittenStreamIsCorrectlyConverted()
        {
            var stream = new MemoryStream();
            stream.Write(new byte[] { 1, 2, 3 }, 0, 3);

            var file = InMemoryFileFactory.CreateFromStream(stream);

            file.GetData().Length.Should().Be(3);
        }
    }
}
