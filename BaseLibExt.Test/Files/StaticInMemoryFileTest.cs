using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseLibExt.Files;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;

namespace BaseLibExt.Test.Files
{
    /// <summary>
    ///     Tests for <see cref="StaticInMemoryFile" />
    /// </summary>
    public class StaticInMemoryFileTest
    {
        /// <summary>
        ///     Check that the data attributes of the <see cref="StaticInMemoryFile" /> are not serialized to JSON.
        /// </summary>
        [Fact]
        public void SerializationIgnoresDataProperties()
        {
            var file = new StaticInMemoryFile("fileName", new byte[100], "contentType");
            var json = JsonConvert.SerializeObject(file);

            json.Should().NotContain("GetData");
            json.Should().NotContain("getData");
            json.Should().NotContain("DataStream");
            json.Should().NotContain("dataStream");
        }
    }
}
