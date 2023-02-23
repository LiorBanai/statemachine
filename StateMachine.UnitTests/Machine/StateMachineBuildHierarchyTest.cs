 
// <copyright file="StateMachineBuildHierarchyTest.cs"  
 

using System;
using FluentAssertions;
using StateMachine.Machine;
using Xunit;

namespace StateMachine.UnitTests.Machine
{
    /// <summary>
    /// Tests hierarchy building in the <see cref="StateMachine{TState,TEvent}"/>.
    /// </summary>
    public class StateMachineBuildHierarchyTest
    {
        /// <summary>
        /// If the super-state is specified as the initial state of its sub-states then an <see cref="ArgumentException"/> is thrown.
        /// </summary>
        [Fact]
        public void AddHierarchicalStatesInitialStateIsSuperStateItself()
        {
            var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<States, Events>();

            Action a = () =>
                stateMachineDefinitionBuilder
                    .DefineHierarchyOn(States.B)
                        .WithHistoryType(HistoryType.None)
                        .WithInitialSubState(States.B)
                        .WithSubState(States.B1)
                        .WithSubState(States.B2);
            a.Should().Throw<ArgumentException>();
        }
    }
}