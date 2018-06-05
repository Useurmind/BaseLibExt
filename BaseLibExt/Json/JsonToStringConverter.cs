using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace BaseLibExt.Json
{
    // <summary>
    ///     A converter that converts any type with the help of <see cref="object.ToString" />.
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class JsonToStringConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
