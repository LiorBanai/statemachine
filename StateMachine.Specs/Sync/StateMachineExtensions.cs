 
// <copyright file="StateMachineExtensions.cs"  
 

using FakeItEasy;
using StateMachine.Machine;
using Xbehave;

namespace StateMachine.Specs.Sync
{
    public class StateMachineExtensions
    {
        [Scenario]
        public void AddingExtensions(
            IStateMachine<string, int> machine,
            IExtension<string, int> extension)
        {
            "establish a state machine".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, int>();
                stateMachineDefinitionBuilder.In("initial");
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("initial")
                    .Build()
                    .CreatePassiveStateMachine();

                extension = A.Fake<IExtension<string, int>>();
            });

            "when adding an extension".x(() =>
            {
                machine.AddExtension(extension);
                machine.Start();
            });

            "it should notify extension about internal events".x(() =>
                A.CallTo(extension)
                    .MustHaveHappened());
        }

        [Scenario]
        public void ClearingExtensions(
            IStateMachine<string, int> machine,
            IExtension<string, int> extension)
        {
            "establish a state machine with an extension".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<string, int>();
                stateMachineDefinitionBuilder.In("initial");
                machine = stateMachineDefinitionBuilder
                    .WithInitialState("initial")
                    .Build()
                    .CreatePassiveStateMachine();

                extension = A.Fake<IExtension<string, int>>();
                machine.AddExtension(extension);
            });

            "when clearing all extensions from the state machine".x(() =>
            {
                machine.ClearExtensions();
                machine.Start();
            });

            "it should not anymore notify extension about internal events".x(() =>
                A.CallTo(extension)
                    .MustNotHaveHappened());
        }
    }
}