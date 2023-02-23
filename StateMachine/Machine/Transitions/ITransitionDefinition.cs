 
// <copyright file="ITransitionDefinition.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Machine.ActionHolders;
using StateMachine.Machine.GuardHolders;
using StateMachine.Machine.States;

namespace StateMachine.Machine.Transitions
{
    public interface ITransitionDefinition<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        IStateDefinition<TState, TEvent> Source { get; }

        IStateDefinition<TState, TEvent> Target { get; }

        IGuardHolder Guard { get; }

        IEnumerable<IActionHolder> Actions { get; }

        bool IsInternalTransition { get; }
    }
}