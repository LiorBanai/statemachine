﻿// <copyright file="Extensions.cs"  

using FakeItEasy;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.States;
using Xbehave;

namespace StateMachine.Specs.Async
{
    public class Extensions
    {
        private const string Name = "machine";

        [Scenario]
        public void BeforeExecutingEntryActions(
            AsyncPassiveStateMachine<string, int> machine,
            IExtension<string, int> extension)
        {
            "establish an extension".x(()
                => extension = A.Fake<IExtension<string, int>>());

            "establish a state machine using the extension".x(async () =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, int>();
                stateMachineDefinitionBuilder
                    .In("0")
                        .On(1)
                        .Goto("1");
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("0")
                    .Build()
                    .CreatePassiveStateMachine(Name);

                machine.AddExtension(extension);

                await machine.Start();
            });

            "when firing an event onto the state machine".x(()
                => machine.Fire(1));

            "it should call EnteringState on registered extensions for target state".x(()
                => A.CallTo(() => extension.EnteringState(
                        A<IStateMachineInformation<string, int>>.That.Matches(x => x.Name == Name && x.CurrentStateId.ExtractOrThrow() == "1"),
                        A<IStateDefinition<string, int>>.That.Matches(x => x.Id == "1"),
                        A<ITransitionContext<string, int>>.That.Matches(x => x.EventId.Value == 1)))
                    .MustHaveHappened());
        }

        [Scenario]
        public void BeforeExecutingEntryActionsHierarchical(
            AsyncPassiveStateMachine<string, string> machine,
            IExtension<string, string> extension)
        {
            "establish an extension".x(()
                => extension = A.Fake<IExtension<string, string>>());

            "establish a hierarchical state machine using the extension".x(async () =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, string>();
                stateMachineDefinitionBuilder
                    .DefineHierarchyOn("A")
                        .WithHistoryType(HistoryType.None)
                        .WithInitialSubState("A0");
                stateMachineDefinitionBuilder
                    .In("0")
                        .On("A0")
                        .Goto("A0");
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("0")
                    .Build()
                    .CreatePassiveStateMachine(Name);

                machine.AddExtension(extension);

                await machine.Start();
            });

            "when firing an event onto the state machine".x(()
                => machine.Fire("A0"));

            "it should call EnteringState on registered extensions for entered super states of target state".x(()
                => A.CallTo(() => extension.EnteringState(
                        A<IStateMachineInformation<string, string>>.That.Matches(x => x.Name == Name && x.CurrentStateId.ExtractOrThrow() == "A0"),
                        A<IStateDefinition<string, string>>.That.Matches(x => x.Id == "A"),
                        A<ITransitionContext<string, string>>.That.Matches(x => x.EventId.Value == "A0")))
                    .MustHaveHappened());

            "it should call EnteringState on registered extensions for entered leaf target state".x(()
                => A.CallTo(() => extension.EnteringState(
                        A<IStateMachineInformation<string, string>>.That.Matches(x => x.Name == Name && x.CurrentStateId.ExtractOrThrow() == "A0"),
                        A<IStateDefinition<string, string>>.That.Matches(x => x.Id == "A0"),
                        A<ITransitionContext<string, string>>.That.Matches(x => x.EventId.Value == "A0")))
                    .MustHaveHappened());
        }
    }
}