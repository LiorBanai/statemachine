 
// <copyright file="InternalExtension.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Infrastructure;
using StateMachine.Machine;
using StateMachine.Machine.States;
using StateMachine.Machine.Transitions;

namespace StateMachine.Extensions
{
    public class InternalExtension<TState, TEvent> : IExtensionInternal<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly IExtension<TState, TEvent> apiExtension;
        private readonly IStateMachineInformation<TState, TEvent> stateMachineInformation;

        public InternalExtension(
            IExtension<TState, TEvent> apiExtension,
            IStateMachineInformation<TState, TEvent> stateMachineInformation)
        {
            this.apiExtension = apiExtension;
            this.stateMachineInformation = stateMachineInformation;
        }

        public void StartedStateMachine()
        {
            this.apiExtension.StartedStateMachine(this.stateMachineInformation);
        }

        public void StoppedStateMachine()
        {
            this.apiExtension.StoppedStateMachine(this.stateMachineInformation);
        }

        public void EventQueued(TEvent eventId, object eventArgument)
        {
            this.apiExtension.EventQueued(this.stateMachineInformation, eventId, eventArgument);
        }

        public void EventQueuedWithPriority(TEvent eventId, object eventArgument)
        {
            this.apiExtension.EventQueuedWithPriority(this.stateMachineInformation, eventId, eventArgument);
        }

        public void SwitchedState(IStateDefinition<TState, TEvent> oldState, IStateDefinition<TState, TEvent> newState)
        {
            this.apiExtension.SwitchedState(this.stateMachineInformation, oldState, newState);
        }

        public void EnteringInitialState(TState state)
        {
            this.apiExtension.EnteringInitialState(this.stateMachineInformation, state);
        }

        public void EnteredInitialState(TState state, ITransitionContext<TState, TEvent> context)
        {
            this.apiExtension.EnteredInitialState(this.stateMachineInformation, state, context);
        }

        public void FiringEvent(ref TEvent eventId, ref object eventArgument)
        {
            this.apiExtension.FiringEvent(this.stateMachineInformation, ref eventId, ref eventArgument);
        }

        public void FiredEvent(ITransitionContext<TState, TEvent> context)
        {
            this.apiExtension.FiredEvent(this.stateMachineInformation, context);
        }

        public void HandlingEntryActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            ref Exception exception)
        {
            this.apiExtension.HandlingEntryActionException(this.stateMachineInformation, stateDefinition, context, ref exception);
        }

        public void HandledEntryActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            Exception exception)
        {
            this.apiExtension.HandledEntryActionException(this.stateMachineInformation, stateDefinition, context, exception);
        }

        public void HandlingExitActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            ref Exception exception)
        {
            this.apiExtension.HandlingExitActionException(this.stateMachineInformation, stateDefinition, context, ref exception);
        }

        public void HandledExitActionException(
            IStateDefinition<TState, TEvent> stateDefinition,
            ITransitionContext<TState, TEvent> context,
            Exception exception)
        {
            this.apiExtension.HandledExitActionException(this.stateMachineInformation, stateDefinition, context, exception);
        }

        public void HandlingGuardException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext,
            ref Exception exception)
        {
            this.apiExtension.HandlingGuardException(this.stateMachineInformation, transitionDefinition, transitionContext, ref exception);
        }

        public void HandledGuardException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext,
            Exception exception)
        {
            this.apiExtension.HandledGuardException(this.stateMachineInformation, transitionDefinition, transitionContext, exception);
        }

        public void HandlingTransitionException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> context,
            ref Exception exception)
        {
            this.apiExtension.HandlingTransitionException(this.stateMachineInformation, transitionDefinition, context, ref exception);
        }

        public void HandledTransitionException(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext,
            Exception exception)
        {
            this.apiExtension.HandledTransitionException(this.stateMachineInformation, transitionDefinition, transitionContext, exception);
        }

        public void SkippedTransition(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> context)
        {
            this.apiExtension.SkippedTransition(this.stateMachineInformation, transitionDefinition, context);
        }

        public void ExecutingTransition(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext)
        {
            this.apiExtension.ExecutingTransition(this.stateMachineInformation, transitionDefinition, transitionContext);
        }

        public void ExecutedTransition(
            ITransitionDefinition<TState, TEvent> transitionDefinition,
            ITransitionContext<TState, TEvent> transitionContext)
        {
            this.apiExtension.ExecutedTransition(this.stateMachineInformation, transitionDefinition, transitionContext);
        }

        public void Loaded(
            IInitializable<TState> loadedCurrentState,
            IReadOnlyDictionary<TState, TState> loadedHistoryStates,
            IReadOnlyCollection<EventInformation<TEvent>> events)
        {
            this.apiExtension.Loaded(this.stateMachineInformation, loadedCurrentState, loadedHistoryStates, events);
        }

        public void EnteringState(IStateDefinition<TState, TEvent> stateDefinition, ITransitionContext<TState, TEvent> context)
        {
            this.apiExtension.EnteringState(this.stateMachineInformation, stateDefinition, context);
        }
    }
}