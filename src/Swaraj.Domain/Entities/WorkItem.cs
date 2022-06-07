using System;

using Newtonsoft.Json;

using Swaraj.Domain.Entities.Identifiers;
using Swaraj.Domain.Enums;

namespace Swaraj.Domain.Entities
{
    static class WorkItemCounter
    {
        private static int currentValue = -1;

        public static int Value => System.Threading.Interlocked.Increment(ref currentValue);
    }

    public class WorkItem : BaseEntity<WorkItemIdentifier>
    {
        public User AssignedTo { get; set; }

        public string Title { get; set; }

        public WorkItemState WorkItemState { get; set; }

        public WorkItemType WorkItemType { get; set; }

        private WorkItem()
           : base()
        { }

        private WorkItem(WorkItem workItem)
        {
            this.AssignedTo = workItem.AssignedTo;
            this.Title = workItem.Title;
            this.WorkItemState = workItem.WorkItemState;
            this.WorkItemType = workItem.WorkItemType;
        }

        /// <summary>
        /// Used to create new workitems with identifier
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public static WorkItem New()
        {
            return new WorkItem()
            { 
                Id = new WorkItemIdentifier(WorkItemCounter.Value),
            };
        }

        /// <summary>
        /// Used to create new workitems with identifier with copied data
        /// </summary>
        /// <param name="workItem"></param>
        /// <returns></returns>
        public static WorkItem Clone(WorkItem workItem)
        {
            var wi = new WorkItem(workItem)
            {
                Id = new WorkItemIdentifier(WorkItemCounter.Value),
            };

            return wi;
        }
    }
}
