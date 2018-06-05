using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BaseLibExt.Files
{
    /// <summary>
    ///     Factory to create <see cref="IInMemoryFile" />.
    /// </summary>
    public static class InMemoryFileFactory
    {
        /// <summary>
        ///     Creates an <see cref="IInMemoryFile" /> from base64 content.
        /// </summary>
        /// <param name="base64Content">Content of the base64.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>The in memory file.</returns>
        public static IInMemoryFile CreateFromBase64(string base64Content, string fileName = null, string contentType = null)
        {
            var data = Convert.FromBase64String(base64Content);

            return CreateFromData(data, fileName, contentType);
        }

        /// <summary>
        /// Creates an in memory file from raw data.
        /// </summary>
        /// <param name="dataStream">The data stream.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>
        /// The in memory file
        /// </returns>
        public static IInMemoryFile CreateFromStream(Stream dataStream, string fileName = null, string contentType = null)
        {
            byte[] data = null;

            dataStream.Seek(0, SeekOrigin.Begin);
            using (BinaryReader br = new BinaryReader(dataStream))
            {
                data = br.ReadBytes((int)dataStream.Length);
            }

            return CreateFromData(data, fileName, contentType);
        }

        /// <summary>
        ///     Creates an in memory file from raw data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>The in memory file</returns>
        public static IInMemoryFile CreateFromData(byte[] data, string fileName = null, string contentType = null)
        {
            contentType = ComputeContentType(fileName, contentType);
            fileName = ComputeFileName(fileName);

            return new StaticInMemoryFile(fileName, data, contentType);
        }

        /// <summary>
        ///     Creates an in memory file from disk.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The in memory file.</returns>
        public static IInMemoryFile CreateFromDisk(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var fileContent = File.ReadAllBytes(filePath);
            return new StaticInMemoryFile(fileName, fileContent, MimeMapping.MimeUtility.GetMimeMapping(fileName));
        }

        /// <summary>
        /// Creates an in memory file from a string.
        /// </summary>
        /// <param name="content">The content of the file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>The in memory file.</returns>
        public static IInMemoryFile CreateFromString(string content, string fileName = null, string contentType = null)
        {
            using (var fs = new MemoryStream())
            {
                byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);

                return new StaticInMemoryFile(fileName, info, contentType);
            }
        }

        private static string ComputeContentType(string fileName, string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = string.IsNullOrEmpty(fileName) ? null : MimeMapping.MimeUtility.GetMimeMapping(fileName);
            }

            return contentType;
        }

        private static string ComputeFileName(string fileName)
        {
            fileName = fileName ?? "noFileNameGive";
            return fileName;
        }
    }
}
