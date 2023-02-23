 
// <copyright file="StandardFactory.cs"  
 

using System;
using StateMachine.Machine.ActionHolders;
using StateMachine.Machine.Contexts;
using StateMachine.Machine.Events;
using StateMachine.Machine.GuardHolders;
using StateMachine.Machine.States;

namespace StateMachine.Machine
{
    /// <summary>
    /// Standard implementation of the state machine factory.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    //// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
    public class StandardFactory<TState, TEvent> : IFactory<TState, TEvent>
        //// ReSharper restore ClassWithVirtualMembersNeverInherited.Global
        where TState : IComparable
        where TEvent : IComparable
    {
        public virtual IActionHolder CreateActionHolder(Action action)
        {
            return new ArgumentLessActionHolder(action);
        }

        public virtual IActionHolder CreateActionHolder<T>(Action<T> action)
        {
            return new ArgumentActionHolder<T>(action);
        }

        public virtual IActionHolder CreateActionHolder<T>(Action<T> action, T parameter)
        {
            return new ParametrizedActionHolder<T>(action, parameter);
        }

        public virtual IActionHolder CreateTransitionActionHolder(Action action)
        {
            return new ArgumentLessActionHolder(action);
        }

        public virtual IActionHolder CreateTransitionActionHolder<T>(Action<T> action)
        {
            return new ArgumentActionHolder<T>(action);
        }

        public virtual IGuardHolder CreateGuardHolder(Func<bool> guard)
        {
            return new ArgumentLessGuardHolder(guard);
        }

        public virtual IGuardHolder CreateGuardHolder<T>(Func<T, bool> guard)
        {
            return new ArgumentGuardHolder<T>(guard);
        }

        public virtual ITransitionContext<TState, TEvent> CreateTransitionContext(IStateDefinition<TState, TEvent> stateDefinition, Missable<TEvent> eventId, object eventArgument, INotifier<TState, TEvent> notifier)
        {
            return new TransitionContext<TState, TEvent>(stateDefinition, eventId, eventArgument, notifier);
        }

        public virtual StateMachineInitializer<TState, TEvent> CreateStateMachineInitializer(IStateDefinition<TState, TEvent> initialState, ITransitionContext<TState, TEvent> context)
        {
            return new StateMachineInitializer<TState, TEvent>(initialState, context);
        }
    }
}