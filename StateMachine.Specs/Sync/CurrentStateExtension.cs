 
// <copyright file="CurrentStateExtension.cs"  
 

using StateMachine.Extensions;
using StateMachine.Machine.States;

namespace StateMachine.Specs.Sync
{
    public class CurrentStateExtension : ExtensionBase<int, int>
    {
        public int CurrentState { get; private set; }

        public override void SwitchedState(IStateMachineInformation<int, int> stateMachine, IStateDefinition<int, int> oldState, IStateDefinition<int, int> newState)
        {
            this.CurrentState = newState.Id;
        }
    }
}