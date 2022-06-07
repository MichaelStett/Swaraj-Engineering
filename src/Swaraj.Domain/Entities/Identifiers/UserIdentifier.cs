using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Swaraj.Domain.Entities.Identifiers
{
    [JsonConverter(typeof(UserIdentifierJsonConverter))]
    [TypeConverter(typeof(UserIdentifierTypeConverter))]
    public class UserIdentifier : EntityIdentifier
    {
        public UserIdentifier(Guid value)
            : base(value)
        {
        }

        class UserIdentifierJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(UserIdentifier);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var id = (UserIdentifier) value;
                serializer.Serialize(writer, id.Value);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var guid = serializer.Deserialize<Guid>(reader);
                return new UserIdentifier(guid);
            }
        }

        class UserIdentifierTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                var stringValue = value as string;
                if (!string.IsNullOrEmpty(stringValue)
                    && Guid.TryParse(stringValue, out var guid))
                {
                    return new UserIdentifier(guid);
                }

                return base.ConvertFrom(context, culture, value);

            }
        }
    }
}
