using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BaseLibExt.Files
{
    /// <summary>
    ///     An in memory representation for a file.
    /// </summary>
    public interface IInMemoryFile
    {
        /// <summary>
        ///     Gets a string for the content type of the file.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        ///     Gets a stream of data via which the file can be retrieved.
        /// </summary>
        Stream DataStream { get; }

        /// <summary>
        ///     Gets the name of the file.
        /// </summary>
        string FileName { get; }

        /// <summary>
        ///     Gets a function to retrieve the data of the file.
        /// </summary>
        Func<byte[]> GetData { get; }
    }
}
