 
// <copyright file="CsvTransitionsWriter.cs"  
 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StateMachine.Machine.States;
using StateMachine.Machine.Transitions;

namespace StateMachine.Machine.Reports
{
    /// <summary>
    /// Writes the transitions of a state machine to a stream as csv.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public class CsvTransitionsWriter<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly StreamWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTransitionsWriter&lt;TState, TEvent&gt;"/> class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public CsvTransitionsWriter(StreamWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Writes the transitions of the specified states.
        /// </summary>
        /// <param name="states">The states.</param>
        public void Write(IEnumerable<IStateDefinition<TState, TEvent>> states)
        {
            states = states.ToList();

            Guard.AgainstNullArgument("states", states);

            this.WriteTransitionsHeader();

            foreach (var state in states)
            {
                this.ReportTransitionsOfState(state);
            }
        }

        private void WriteTransitionsHeader()
        {
            this.writer.WriteLine("Source;Event;Guard;Target;Actions");
        }

        private void ReportTransitionsOfState(IStateDefinition<TState, TEvent> state)
        {
            foreach (var transition in state.TransitionInfos)
            {
                this.ReportTransition(transition);
            }
        }

        private void ReportTransition(TransitionInfo<TState, TEvent> transition)
        {
            string source = transition.Source.ToString();
            string target = transition.Target != null ? transition.Target.Id.ToString() : "internal transition";
            string eventId = transition.EventId.ToString();

            string guard = transition.Guard != null ? transition.Guard.Describe() : string.Empty;
            string actions = string.Join(", ", transition.Actions.Select(action => action.Describe()));

            this.writer.WriteLine(
                "{0};{1};{2};{3};{4}",
                source,
                eventId,
                guard,
                target,
                actions);
        }
    }
}