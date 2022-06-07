using System;

namespace Swaraj.Domain
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}
