 
// <copyright file="StateMachineAssertionsExtensionMethods.cs"  
 

using System;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using StateMachine.Machine;
using StateMachine.Machine.States;

namespace StateMachine.UnitTests
{
    public static class StateMachineAssertionsExtensionMethods
    {
        public static void BeSuccessfulTransitionResultWithNewState<TStates, TEvents>(this ObjectAssertions assertions, IStateDefinition<TStates, TEvents> expectedNewState)
            where TStates : IComparable
            where TEvents : IComparable
        {
            var transitionResult = (ITransitionResult<TStates>)assertions.Subject;

            Execute.Assertion
                   .ForCondition(transitionResult.Fired)
                   .FailWith("expected successful (fired) transition result.");

            Execute.Assertion
                   .ForCondition(transitionResult.NewState.CompareTo(expectedNewState.Id) == 0)
                   .FailWith("expected transition result with new state = `" + expectedNewState.Id + "`, but found `" + transitionResult.NewState + "`.");
        }

        public static void BeNotFiredTransitionResult<TStates>(this ObjectAssertions assertions)
            where TStates : IComparable
        {
            var transitionResult = (ITransitionResult<TStates>)assertions.Subject;

            Execute.Assertion
                   .ForCondition(!transitionResult.Fired)
                   .FailWith("expected not fired transition result.");
        }
    }
}