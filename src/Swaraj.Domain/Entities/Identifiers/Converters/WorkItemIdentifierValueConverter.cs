using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swaraj.Domain.Entities.Identifiers.Converters
{
    public class WorkItemIdentifierValueConverter : ValueConverter<WorkItemIdentifier, int>
    {
        public WorkItemIdentifierValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                id => IntToGuidConverter.ConvertBack(id.Value),
                value => new WorkItemIdentifier(value),
                mappingHints
            )
        { }
    }
}
