namespace Swaraj.Domain.Entities.Identifiers
{
    public class WorkItemIdentifier : EntityIdentifier
    {
        public WorkItemIdentifier(int value)
            : base(IntToGuidConverter.Convert(value))
        {
        }
    }
}
