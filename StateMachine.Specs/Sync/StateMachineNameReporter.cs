 
// <copyright file="StateMachineNameReporter.cs"  
 

using System.Collections.Generic;
using StateMachine.Machine;
using StateMachine.Machine.States;

namespace StateMachine.Specs.Sync
{
    public class StateMachineNameReporter : IStateMachineReport<string, int>
    {
        public string StateMachineName { get; private set; }

        public void Report(string name, IEnumerable<IStateDefinition<string, int>> states, string initialStateId)
        {
            this.StateMachineName = name;
        }
    }
}