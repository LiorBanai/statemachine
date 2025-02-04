 
// <copyright file="CsvStateMachineReportGeneratorTest.cs"  
 

using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using StateMachine.Machine;
using StateMachine.Machine.Reports;
using Xunit;

namespace StateMachine.UnitTests.Machine.Reports
{
    public class CsvStateMachineReportGeneratorTest
    {
        /// <summary>
        /// Some test states for simulating an elevator.
        /// </summary>
        public enum States
        {
            /// <summary>Elevator has an Error</summary>
            Error,

            /// <summary>Elevator is healthy, i.e. no error</summary>
            Healthy,

            /// <summary>The elevator is moving (either up or down)</summary>
            Moving,

            /// <summary>The elevator is moving down.</summary>
            MovingUp,

            /// <summary>The elevator is moving down.</summary>
            MovingDown,

            /// <summary>The elevator is standing on a floor.</summary>
            OnFloor,

            /// <summary>The door is closed while standing still.</summary>
            DoorClosed,

            /// <summary>The door is open while standing still.</summary>
            DoorOpen
        }

        /// <summary>
        /// Some test events for the elevator.
        /// </summary>
        public enum Events
        {
            /// <summary>An error occurred.</summary>
            ErrorOccurred,

            /// <summary>Reset after error.</summary>
            Reset,

            /// <summary>Open the door.</summary>
            OpenDoor,

            /// <summary>Close the door.</summary>
            CloseDoor,

            /// <summary>Move elevator up.</summary>
            GoUp,

            /// <summary>Move elevator down.</summary>
            GoDown,

            /// <summary>Stop the elevator.</summary>
            Stop
        }

        public static IEnumerable<object[]> StateMachineInstantiationProvider =>
            new List<object[]>
            {
                new object[] { "PassiveStateMachine", new Func<string, StateMachineDefinition<States, Events>, IStateMachine<States, Events>>((name, smd) => smd.CreatePassiveStateMachine(name)) },
                new object[] { "ActiveStateMachine", new Func<string, StateMachineDefinition<States, Events>, IStateMachine<States, Events>>((name, smd) => smd.CreateActiveStateMachine(name)) }
            };

        [Theory]
        [MemberData(nameof(StateMachineInstantiationProvider))]
        public void Report(string dummyName, Func<string, StateMachineDefinition<States, Events>, IStateMachine<States, Events>> createStateMachine)
        {
            var stateStream = new MemoryStream();
            var transitionsStream = new MemoryStream();

            var testee = new CsvStateMachineReportGenerator<States, Events>(stateStream, transitionsStream);

            var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<States, Events>();
            stateMachineDefinitionBuilder
                .DefineHierarchyOn(States.Healthy)
                    .WithHistoryType(HistoryType.Deep)
                    .WithInitialSubState(States.OnFloor)
                    .WithSubState(States.Moving);
            stateMachineDefinitionBuilder
                .DefineHierarchyOn(States.Moving)
                    .WithHistoryType(HistoryType.Shallow)
                    .WithInitialSubState(States.MovingUp)
                    .WithSubState(States.MovingDown);
            stateMachineDefinitionBuilder
                .DefineHierarchyOn(States.OnFloor)
                    .WithHistoryType(HistoryType.None)
                    .WithInitialSubState(States.DoorClosed)
                    .WithSubState(States.DoorOpen);
            stateMachineDefinitionBuilder
                .In(States.Healthy)
                    .On(Events.ErrorOccurred).Goto(States.Error);
            stateMachineDefinitionBuilder
                .In(States.Error)
                    .On(Events.Reset).Goto(States.Healthy)
                    .On(Events.ErrorOccurred);
            stateMachineDefinitionBuilder
                .In(States.OnFloor)
                    .ExecuteOnEntry(AnnounceFloor)
                    .ExecuteOnExit(Beep)
                    .ExecuteOnExit(Beep)
                    .On(Events.CloseDoor).Goto(States.DoorClosed)
                    .On(Events.OpenDoor).Goto(States.DoorOpen)
                    .On(Events.GoUp)
                        .If(CheckOverload)
                            .Goto(States.MovingUp)
                        .Otherwise()
                            .Execute(AnnounceOverload)
                            .Execute(Beep)
                    .On(Events.GoDown)
                        .If(CheckOverload)
                            .Goto(States.MovingDown)
                        .Otherwise()
                            .Execute(AnnounceOverload);
            stateMachineDefinitionBuilder
                .In(States.Moving)
                    .On(Events.Stop).Goto(States.OnFloor);
            var stateMachineDefinition = stateMachineDefinitionBuilder
                .WithInitialState(States.OnFloor)
                .Build();

            var elevator = createStateMachine("Elevator", stateMachineDefinition);

            elevator.Report(testee);

            string statesReport;
            string transitionsReport;
            stateStream.Position = 0;
            using (var reader = new StreamReader(stateStream))
            {
                statesReport = reader.ReadToEnd();
            }

            transitionsStream.Position = 0;
            using (var reader = new StreamReader(transitionsStream))
            {
                transitionsReport = reader.ReadToEnd();
            }

            const string ExpectedTransitionsReport = "Source;Event;Guard;Target;ActionsHealthy;ErrorOccurred;;Error;OnFloor;CloseDoor;;DoorClosed;OnFloor;OpenDoor;;DoorOpen;OnFloor;GoUp;CheckOverload;MovingUp;OnFloor;GoUp;;internal transition;AnnounceOverload, BeepOnFloor;GoDown;CheckOverload;MovingDown;OnFloor;GoDown;;internal transition;AnnounceOverloadMoving;Stop;;OnFloor;Error;Reset;;Healthy;Error;ErrorOccurred;;internal transition;";
            const string ExpectedStatesReport = "Source;Entry;Exit;ChildrenHealthy;;;OnFloor, MovingOnFloor;AnnounceFloor;Beep, Beep;DoorClosed, DoorOpenMoving;;;MovingUp, MovingDownMovingUp;;;MovingDown;;;DoorClosed;;;DoorOpen;;;Error;;;";

            statesReport
                .IgnoringNewlines()
                .Should()
                .Be(
                    ExpectedStatesReport
                        .IgnoringNewlines());

            transitionsReport
                .IgnoringNewlines()
                .Should()
                .Be(
                    ExpectedTransitionsReport
                        .IgnoringNewlines());

            stateStream.Dispose();
        }

        private static void Beep()
        {
        }

        private static bool CheckOverload()
        {
            return true;
        }

        private static void AnnounceFloor()
        {
        }

        private static void AnnounceOverload()
        {
        }
    }
}