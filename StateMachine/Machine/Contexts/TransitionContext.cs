 
// <copyright file="TransitionContext.cs"  
 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using StateMachine.Machine.States;

namespace StateMachine.Machine.Contexts
{
    /// <summary>
    /// Provides context information during a transition.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    [DebuggerDisplay("State = {StateDefinition} Event = {EventId} EventArguments = {EventArgument}")]
    public class TransitionContext<TState, TEvent> : ITransitionContext<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly List<Record> records = new List<Record>();

        public TransitionContext(IStateDefinition<TState, TEvent> stateDefinition, Missable<TEvent> eventId, object eventArgument, INotifier<TState, TEvent> notifier)
        {
            this.StateDefinition = stateDefinition;
            this.EventId = eventId;
            this.EventArgument = eventArgument;
            this.Notifier = notifier;
        }

        public IStateDefinition<TState, TEvent> StateDefinition { get; }

        public Missable<TEvent> EventId { get; }

        public object EventArgument { get; }

        public INotifier<TState, TEvent> Notifier { get; }

        public void OnExceptionThrown(Exception exception)
        {
            this.Notifier.OnExceptionThrown(this, exception);
        }

        public void OnTransitionBegin()
        {
            this.Notifier.OnTransitionBegin(this);
        }

        public void AddRecord(TState stateId, RecordType recordType)
        {
            this.records.Add(new Record(stateId, recordType));
        }

        public string GetRecords()
        {
            StringBuilder result = new StringBuilder();

            this.records.ForEach(record => result.AppendFormat(" -> {0}", record));

            return result.ToString();
        }

        private class Record
        {
            public Record(TState stateId, RecordType recordType)
            {
                this.StateId = stateId;
                this.RecordType = recordType;
            }

            private TState StateId { get; set; }

            private RecordType RecordType { get; set; }

            public override string ToString()
            {
                return this.RecordType + " " + this.StateId;
            }
        }
    }
}