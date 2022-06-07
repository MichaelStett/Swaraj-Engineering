using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Swaraj.Domain.Entities.Identifiers.Converters
{
    public class UserIdentifierValueConverter : ValueConverter<UserIdentifier, Guid>
    {
        public UserIdentifierValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                id => id.Value,
                value => new UserIdentifier(value),
                mappingHints
            )
        { }
    }
}
