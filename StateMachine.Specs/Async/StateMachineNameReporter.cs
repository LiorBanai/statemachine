 
// <copyright file="StateMachineNameReporter.cs"  
 

using System.Collections.Generic;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.States;

namespace StateMachine.Specs.Async
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