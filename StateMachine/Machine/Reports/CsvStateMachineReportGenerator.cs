 
// <copyright file="CsvStateMachineReportGenerator.cs"  
 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StateMachine.Machine.States;

namespace StateMachine.Machine.Reports
{
    /// <summary>
    /// Generator for csv reports of states and transitions of a state machine.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    public class CsvStateMachineReportGenerator<TState, TEvent> : IStateMachineReport<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        private readonly Stream statesStream;

        private readonly Stream transitionsStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvStateMachineReportGenerator{TState, TEvent}"/> class.
        /// </summary>
        /// <param name="statesStream">The stream where the states are written to.</param>
        /// <param name="transitionsStream">The stream where the transitions are written to.</param>
        public CsvStateMachineReportGenerator(Stream statesStream, Stream transitionsStream)
        {
            this.statesStream = statesStream;
            this.transitionsStream = transitionsStream;
        }

        /// <summary>
        /// Generates a report of the state machine.
        /// </summary>
        /// <param name="name">The name of the state machine.</param>
        /// <param name="states">The states.</param>
        /// <param name="initialStateId">The initial state id.</param>
        public void Report(string name, IEnumerable<IStateDefinition<TState, TEvent>> states, TState initialStateId)
        {
            states = states.ToList();

            this.ReportStates(states);
            this.ReportTransitions(states);
        }

        private void ReportStates(IEnumerable<IStateDefinition<TState, TEvent>> states)
        {
            var writer = new StreamWriter(this.statesStream);

            var statesWriter = new CsvStatesWriter<TState, TEvent>(writer);

            statesWriter.Write(states);

            writer.Flush();
        }

        private void ReportTransitions(IEnumerable<IStateDefinition<TState, TEvent>> states)
        {
            var writer = new StreamWriter(this.transitionsStream);

            var transitionsWriter = new CsvTransitionsWriter<TState, TEvent>(writer);

            transitionsWriter.Write(states);

            writer.Flush();
        }
    }
}