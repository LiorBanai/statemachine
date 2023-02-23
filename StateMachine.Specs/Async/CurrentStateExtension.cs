 
// <copyright file="CurrentStateExtension.cs"  
 

using System.Threading.Tasks;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.States;

namespace StateMachine.Specs.Async
{
    public class CurrentStateExtension : AsyncExtensionBase<int, int>
    {
        public int CurrentState { get; private set; }

        public override Task SwitchedState(IStateMachineInformation<int, int> stateMachine, IStateDefinition<int, int> oldState, IStateDefinition<int, int> newState)
        {
            this.CurrentState = newState.Id;

            return Task.CompletedTask;
        }
    }
}