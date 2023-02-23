 
// <copyright file="CsvStatesWriter.cs"  
 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StateMachine.Machine.States;

namespace StateMachine.Machine.Reports
{
    /// <summary>
    /// Writes the states of a state machine to a stream as csv.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public class CsvStatesWriter<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly StreamWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvStatesWriter&lt;TState, TEvent&gt;"/> class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public CsvStatesWriter(StreamWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Writes the specified states.
        /// </summary>
        /// <param name="states">The states.</param>
        public void Write(IEnumerable<IStateDefinition<TState, TEvent>> states)
        {
            states = states.ToList();

            Guard.AgainstNullArgument("states", states);

            this.WriteStatesHeader();

            foreach (var state in states)
            {
                this.ReportState(state);
            }
        }

        private void WriteStatesHeader()
        {
            this.writer.WriteLine("Source;Entry;Exit;Children");
        }

        private void ReportState(IStateDefinition<TState, TEvent> state)
        {
            var entry = string.Join(", ", state.EntryActions.Select(action => action.Describe()));
            var exit = string.Join(", ", state.ExitActions.Select(action => action.Describe()));
            var children = string.Join(", ", state.SubStates.Select(s => s.Id.ToString()));

            this.writer.WriteLine(
                "{0};{1};{2};{3}",
                state.Id,
                entry,
                exit,
                children);
        }
    }
}