using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

using Newtonsoft.Json;

namespace BaseLibExt.Files
{
    /// <summary>
    ///     This in memory file encapsulates a email attachment for the system.net.mail namespace.
    /// </summary>
    public class AttachementInMemoryFile : IInMemoryFile
    {
        private Attachment attachment;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AttachementInMemoryFile" /> class.
        /// </summary>
        /// <param name="attachment">The attachment.</param>
        public AttachementInMemoryFile(Attachment attachment)
        {
            this.attachment = attachment;
        }

        /// <inheritdoc />
        public string ContentType
        {
            get
            {
                return this.attachment.ContentType.Name;
            }
        }

        /// <inheritdoc />
        [JsonIgnore]
        public Stream DataStream
        {
            get
            {
                return this.attachment.ContentStream;
            }
        }

        /// <inheritdoc />
        public string FileName
        {
            get
            {
                return this.attachment.Name;
            }
        }

        /// <inheritdoc />
        [JsonIgnore]
        public Func<byte[]> GetData
        {
            get
            {
                return () =>
                {
                    var memoryStream = new MemoryStream();
                    this.attachment.ContentStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                };
            }
        }

        /// <summary>
        ///     Converts this instance back into the system mail attachment.
        /// </summary>
        /// <returns>The original attachment.</returns>
        public Attachment ToMailAttachment()
        {
            return this.attachment;
        }
    }
}
