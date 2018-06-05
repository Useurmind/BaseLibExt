using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace BaseLibExt.Files
{
    /// <summary>
    ///     In memory file that directly contains the file as data.
    /// </summary>
    /// <seealso cref="IInMemoryFile" />
    public class StaticInMemoryFile : IInMemoryFile
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StaticInMemoryFile" /> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="data">The data.</param>
        /// <param name="contentType">Type of the content.</param>
        public StaticInMemoryFile(string fileName, byte[] data, string contentType = null)
        {
            this.FileName = fileName;
            this.GetData = () => data;
            this.ContentType = contentType;
        }

        /// <inheritdoc />
        public string ContentType { get; private set; }

        /// <inheritdoc />
        [JsonIgnore]
        public Stream DataStream
        {
            get
            {
                return new MemoryStream(this.GetData());
            }
        }

        /// <inheritdoc />
        public string FileName { get; private set; }

        /// <inheritdoc />
        [JsonIgnore]
        public Func<byte[]> GetData { get; private set; }
    }
}
