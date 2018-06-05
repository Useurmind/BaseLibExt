using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace BaseLibExt.Files
{
    /// <summary>
    ///     Extensions for IInMemoryFile.
    /// </summary>
    public static class InMemoryFileExtensions
    {
        /// <summary>
        ///     Converts a file into an system.net.mail attachment.
        /// </summary>
        /// <param name="inMemoryFile">The in memory file.</param>
        /// <returns>The attachement</returns>
        public static Attachment ToMailAttachment(this IInMemoryFile inMemoryFile)
        {
            var attachementInMemoryFile = inMemoryFile as AttachementInMemoryFile;
            if (attachementInMemoryFile != null)
            {
                return attachementInMemoryFile.ToMailAttachment();
            }
            else
            {
                return new Attachment(inMemoryFile.DataStream, inMemoryFile.FileName, inMemoryFile.ContentType);
            }
        }

        /// <summary>
        ///     Writes the in memory file to a file on disk.
        /// </summary>
        /// <param name="inMemoryFile">The in memory file.</param>
        public static void WriteToFile(this IInMemoryFile inMemoryFile)
        {
            using (var file = File.Create(inMemoryFile.FileName))
            {
                var data = inMemoryFile.GetData();
                file.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// Gets the content of the file as base64 string.
        /// </summary>
        /// <param name="inMemoryFile">The in memory file.</param>
        /// <returns>The content as base64 string.</returns>
        public static string GetDataAsBase64(this IInMemoryFile inMemoryFile)
        {
            return Convert.ToBase64String(inMemoryFile.GetData());
        }
    }
}
