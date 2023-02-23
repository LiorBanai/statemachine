 
// <copyright file="RecordEventsExtension.cs"  
 

using System.Collections.Generic;
using StateMachine.Extensions;
using StateMachine.Machine;

namespace StateMachine.Specs.Sync
{
    public class RecordEventsExtension : ExtensionBase<int, int>
    {
        public RecordEventsExtension()
        {
            this.RecordedFiredEvents = new List<int>();
            this.RecordedQueuedEvents = new List<int>();
        }

        public IList<int> RecordedFiredEvents { get; private set; }

        public IList<int> RecordedQueuedEvents { get; private set; }

        public override void FiredEvent(IStateMachineInformation<int, int> stateMachine, ITransitionContext<int, int> context)
        {
            this.RecordedFiredEvents.Add(context.EventId.Value);
        }

        public override void EventQueued(IStateMachineInformation<int, int> stateMachine, int eventId, object eventArgument)
        {
            this.RecordedQueuedEvents.Add(eventId);
        }
    }
}