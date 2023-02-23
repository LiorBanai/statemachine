 
// <copyright file="HierarchyBuilder.cs"  
 

using System;
using System.Collections.Generic;
using StateMachine.Machine.States;
using StateMachine.Syntax;

namespace StateMachine.Machine
{
    public class HierarchyBuilder<TState, TEvent> :
        IHierarchySyntax<TState>,
        IInitialSubStateSyntax<TState>,
        ISubStateSyntax<TState>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly StateDefinition<TState, TEvent> superState;

        private readonly IImplicitAddIfNotAvailableStateDefinitionDictionary<TState, TEvent> stateDefinitions;
        private readonly IDictionary<TState, TState> initiallyLastActiveStates;

        public HierarchyBuilder(
            TState superStateId,
            IImplicitAddIfNotAvailableStateDefinitionDictionary<TState, TEvent> stateDefinitions,
            IDictionary<TState, TState> initiallyLastActiveStates)
        {
            Guard.AgainstNullArgument("states", stateDefinitions);

            this.stateDefinitions = stateDefinitions;
            this.initiallyLastActiveStates = initiallyLastActiveStates;
            this.superState = this.stateDefinitions[superStateId];
        }

        public IInitialSubStateSyntax<TState> WithHistoryType(HistoryType historyType)
        {
            this.superState.HistoryTypeModifiable = historyType;

            return this;
        }

        public ISubStateSyntax<TState> WithInitialSubState(TState stateId)
        {
            this.WithSubState(stateId);

            this.superState.InitialStateModifiable = this.stateDefinitions[stateId];
            this.initiallyLastActiveStates.Add(this.superState.Id, stateId);

            return this;
        }

        public ISubStateSyntax<TState> WithSubState(TState stateId)
        {
            var subState = this.stateDefinitions[stateId];

            this.CheckThatStateHasNotAlreadyASuperState(subState);

            subState.SuperStateModifiable = this.superState;
            this.superState.SubStatesModifiable.Add(subState);

            return this;
        }

        private void CheckThatStateHasNotAlreadyASuperState(StateDefinition<TState, TEvent> subState)
        {
            Guard.AgainstNullArgument("subState", subState);

            if (subState.SuperState != null)
            {
                throw new InvalidOperationException(
                    ExceptionMessages.CannotSetStateAsASuperStateBecauseASuperStateIsAlreadySet(
                        this.superState.Id,
                        subState));
            }
        }
    }
}