 
// <copyright file="IStateDefinitionDictionary.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Machine.States;

namespace StateMachine.Machine
{
    public interface IStateDefinitionDictionary<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        IStateDefinition<TState, TEvent> this[TState key] { get; }

        IEnumerable<IStateDefinition<TState, TEvent>> Values { get; }
    }
}