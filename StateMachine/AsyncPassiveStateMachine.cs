﻿// <copyright file="AsyncPassiveStateMachine.cs" company="Appccelerate">
//   Copyright (c) 2008-2019 Appccelerate
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.Events;
using StateMachine.Persistence;

namespace StateMachine
{
    public class AsyncPassiveStateMachine<TState, TEvent> : IAsyncStateMachine<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly StateMachine<TState, TEvent> stateMachine;

        private readonly StateContainer<TState, TEvent> stateContainer;

        private readonly IStateDefinitionDictionary<TState, TEvent> stateDefinitions;

        private readonly TState initialState;

        private bool executing;

        public AsyncPassiveStateMachine(
            StateMachine<TState, TEvent> stateMachine,
            StateContainer<TState, TEvent> stateContainer,
            IStateDefinitionDictionary<TState, TEvent> stateDefinitions,
            TState initialState)
        {
            this.stateMachine = stateMachine;
            this.stateContainer = stateContainer;
            this.stateDefinitions = stateDefinitions;
            this.initialState = initialState;
        }

        /// <summary>
        /// Occurs when no transition could be executed.
        /// </summary>
        public event EventHandler<TransitionEventArgs<TState, TEvent>> TransitionDeclined
        {
            add => this.stateMachine.TransitionDeclined += value;
            remove => this.stateMachine.TransitionDeclined -= value;
        }

        /// <summary>
        /// Occurs when an exception was thrown inside a transition of the state machine.
        /// </summary>
        public event EventHandler<TransitionExceptionEventArgs<TState, TEvent>> TransitionExceptionThrown
        {
            add => this.stateMachine.TransitionExceptionThrown += value;
            remove => this.stateMachine.TransitionExceptionThrown -= value;
        }

        /// <summary>
        /// Occurs when a transition begins.
        /// </summary>
        public event EventHandler<TransitionEventArgs<TState, TEvent>> TransitionBegin
        {
            add => this.stateMachine.TransitionBegin += value;
            remove => this.stateMachine.TransitionBegin -= value;
        }

        /// <summary>
        /// Occurs when a transition completed.
        /// </summary>
        public event EventHandler<TransitionCompletedEventArgs<TState, TEvent>> TransitionCompleted
        {
            add => this.stateMachine.TransitionCompleted += value;
            remove => this.stateMachine.TransitionCompleted -= value;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is running. The state machine is running if if was started and not yet stopped.
        /// </summary>
        /// <value><c>true</c> if this instance is running; otherwise, <c>false</c>.</value>
        public bool IsRunning
        {
            get; private set;
        }

        /// <summary>
        /// Fires the specified event.
        /// </summary>
        /// <param name="eventId">The event.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Fire(TEvent eventId)
        {
            await this.Fire(eventId, Missing.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Fires the specified event.
        /// </summary>
        /// <param name="eventId">The event.</param>
        /// <param name="eventArgument">The event argument.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Fire(TEvent eventId, object eventArgument)
        {
            this.stateContainer.Events.Enqueue(new EventInformation<TEvent>(eventId, eventArgument));

            await this.stateContainer
                .ForEach(extension => extension.EventQueued(eventId, eventArgument))
                .ConfigureAwait(false);

            await this.Execute().ConfigureAwait(false);
        }

        /// <summary>
        /// Fires the specified priority event. The event will be handled before any already queued event.
        /// </summary>
        /// <param name="eventId">The event.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task FirePriority(TEvent eventId)
        {
            await this.FirePriority(eventId, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Fires the specified priority event. The event will be handled before any already queued event.
        /// </summary>
        /// <param name="eventId">The event.</param>
        /// <param name="eventArgument">The event argument.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task FirePriority(TEvent eventId, object eventArgument)
        {
            this.stateContainer.PriorityEvents.Push(new EventInformation<TEvent>(eventId, eventArgument));

            await this.stateContainer
                .ForEach(extension => extension.EventQueuedWithPriority(eventId, eventArgument))
                .ConfigureAwait(false);

            await this.Execute().ConfigureAwait(false);
        }

        /// <summary>
        /// Starts the state machine. Events will be processed.
        /// If the state machine is not started then the events will be queued until the state machine is started.
        /// Already queued events are processed.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Start()
        {
            this.IsRunning = true;

            await this.stateContainer.ForEach(extension => extension.StartedStateMachine())
                .ConfigureAwait(false);

            await this.Execute().ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a state machine report with the specified generator.
        /// </summary>
        /// <param name="reportGenerator">The report generator.</param>
        public void Report(IStateMachineReport<TState, TEvent> reportGenerator)
        {
            reportGenerator.Report(this.ToString(), this.stateDefinitions.Values, this.initialState);
        }

        public override string ToString()
        {
            return this.stateContainer.Name ?? this.GetType().FullName;
        }

        /// <summary>
        /// Stops the state machine. Events will be queued until the state machine is started.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Stop()
        {
            this.IsRunning = false;

            await this.stateContainer.ForEach(extension => extension.StoppedStateMachine())
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Adds an extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        public void AddExtension(IExtension<TState, TEvent> extension)
        {
            var extensionCompose = new InternalExtension<TState, TEvent>(extension, this.stateContainer);
            this.stateContainer.Extensions.Add(extensionCompose);
        }

        /// <summary>
        /// Clears all extensions.
        /// </summary>
        public void ClearExtensions()
        {
            this.stateContainer.Extensions.Clear();
        }

        /// <summary>
        /// Saves the current state and history states to a persisted state. Can be restored using <see cref="Load"/>.
        /// </summary>
        /// <param name="stateMachineSaver">Data to be persisted is passed to the saver.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Save(IAsyncStateMachineSaver<TState, TEvent> stateMachineSaver)
        {
            Guard.AgainstNullArgument(nameof(stateMachineSaver), stateMachineSaver);

            await stateMachineSaver.SaveCurrentState(this.stateContainer.CurrentStateId)
                .ConfigureAwait(false);

            await stateMachineSaver.SaveHistoryStates(this.stateContainer.LastActiveStates)
                .ConfigureAwait(false);

            await stateMachineSaver.SaveEvents(this.stateContainer.SaveableEvents)
                .ConfigureAwait(false);

            await stateMachineSaver.SavePriorityEvents(this.stateContainer.SaveablePriorityEvents)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Loads the current state and history states from a persisted state (<see cref="Save"/>).
        /// The loader should return exactly the data that was passed to the saver.
        /// </summary>
        /// <param name="stateMachineLoader">Loader providing persisted data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Load(IAsyncStateMachineLoader<TState, TEvent> stateMachineLoader)
        {
            Guard.AgainstNullArgument(nameof(stateMachineLoader), stateMachineLoader);

            this.CheckThatNotAlreadyInitialized();

            var loadedCurrentState = await stateMachineLoader.LoadCurrentState().ConfigureAwait(false);
            var historyStates = await stateMachineLoader.LoadHistoryStates().ConfigureAwait(false);
            var events = await stateMachineLoader.LoadEvents().ConfigureAwait(false);
            var priorityEvents = await stateMachineLoader.LoadPriorityEvents().ConfigureAwait(false);

            SetCurrentState();
            SetHistoryStates();
            SetEvents();
            NotifyExtensions();

            void SetCurrentState()
            {
                this.stateContainer.CurrentStateId = loadedCurrentState;
            }

            void SetEvents()
            {
                this.stateContainer.Events = new ConcurrentQueue<EventInformation<TEvent>>(events);
                this.stateContainer.PriorityEvents = new ConcurrentStack<EventInformation<TEvent>>(priorityEvents);
            }

            void SetHistoryStates()
            {
                foreach (var historyState in historyStates)
                {
                    var superState = this.stateDefinitions[historyState.Key];
                    var lastActiveState = historyState.Value;

                    var lastActiveStateIsNotASubStateOfSuperState = superState
                                                                        .SubStates
                                                                        .Select(x => x.Id)
                                                                        .Contains(lastActiveState)
                                                                    == false;
                    if (lastActiveStateIsNotASubStateOfSuperState)
                    {
                        throw new InvalidOperationException(ExceptionMessages.CannotSetALastActiveStateThatIsNotASubState);
                    }

                    this.stateContainer.SetLastActiveStateFor(superState.Id, lastActiveState);
                }
            }

            void NotifyExtensions()
            {
                this.stateContainer.Extensions.ForEach(
                    extension => extension.Loaded(
                        loadedCurrentState,
                        historyStates,
                        events,
                        priorityEvents));
            }
        }

        private void CheckThatNotAlreadyInitialized()
        {
            if (this.stateContainer.CurrentStateId.IsInitialized)
            {
                throw new InvalidOperationException(ExceptionMessages.StateMachineIsAlreadyInitialized);
            }
        }

        private async Task Execute()
        {
            if (this.executing || !this.IsRunning)
            {
                return;
            }

            this.executing = true;
            try
            {
                await this.ProcessQueuedEvents().ConfigureAwait(false);
            }
            finally
            {
                this.executing = false;
            }
        }

        private async Task ProcessQueuedEvents()
        {
            await this.InitializeStateMachineIfInitializationIsPending()
                .ConfigureAwait(false);

            while (this.stateContainer.PriorityEvents.TryPop(out var eventInformation))
            {
                await this.stateMachine.Fire(
                        eventInformation.EventId,
                        eventInformation.EventArgument,
                        this.stateContainer,
                        this.stateDefinitions)
                    .ConfigureAwait(false);
            }

            while (this.stateContainer.Events.TryDequeue(out var eventInformation))
            {
                await this.stateMachine.Fire(
                        eventInformation.EventId,
                        eventInformation.EventArgument,
                        this.stateContainer,
                        this.stateDefinitions)
                    .ConfigureAwait(false);
            }
        }

        private async Task InitializeStateMachineIfInitializationIsPending()
        {
            if (this.stateContainer.CurrentStateId.IsInitialized)
            {
                return;
            }

            await this.stateMachine.EnterInitialState(this.stateContainer, this.stateDefinitions, this.initialState)
                .ConfigureAwait(false);
        }
    }
}