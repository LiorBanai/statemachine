 
// <copyright file="RecordEventsExtension.cs"  
 

using System.Collections.Generic;
using System.Threading.Tasks;
using StateMachine.AsyncMachine;

namespace StateMachine.Specs.Async
{
    public class RecordEventsExtension : AsyncExtensionBase<int, int>
        {
            public RecordEventsExtension()
            {
                this.RecordedFiredEvents = new List<int>();
                this.RecordedQueuedEvents = new List<int>();
            }

            public IList<int> RecordedFiredEvents { get; }

            public IList<int> RecordedQueuedEvents { get; }

            public override Task FiredEvent(IStateMachineInformation<int, int> stateMachine, ITransitionContext<int, int> context)
            {
                this.RecordedFiredEvents.Add(context.EventId.Value);

                return Task.CompletedTask;
            }

            public override Task EventQueued(IStateMachineInformation<int, int> stateMachine, int eventId, object eventArgument)
            {
                this.RecordedQueuedEvents.Add(eventId);

                return Task.CompletedTask;
            }
        }
}