 
// <copyright file="StateContainerTest.cs"  
 

using FakeItEasy;
using FluentAssertions;
using StateMachine.Infrastructure;
using StateMachine.Machine;
using Xunit;

namespace StateMachine.UnitTests.Machine
{
    public class StateContainerTest
    {
        [Fact]
        public void ReturnsName()
        {
            const string Name = "container";
            var stateContainer = new StateContainer<string, int>(Name);

            stateContainer
                .Name
                .Should()
                .Be(Name);
        }

        [Fact]
        public void ReturnsNothingAsLastActiveStateWhenStateWasNeverSet()
        {
            var stateContainer = new StateContainer<string, int>();

            stateContainer.SetLastActiveStateFor("A", "helloWorld");

            stateContainer
                .GetLastActiveStateFor("B")
                .Should()
                .BeEquivalentTo(Optional<string>.Nothing());
        }

        [Fact]
        public void ReturnsStateXAsLastActiveStateWhenXWasSetBefore()
        {
            var stateContainer = new StateContainer<string, int>();

            stateContainer.SetLastActiveStateFor("A", "Z");

            stateContainer
                .GetLastActiveStateFor("A")
                .Should()
                .BeEquivalentTo(Optional<string>.Just("Z"));
        }

        [Fact]
        public void ExtensionsWhenExtensionsAreClearedThenNoExtensionIsRegistered()
        {
            var executed = false;
            var extension = A.Fake<IExtensionInternal<string, int>>();

            var testee = new StateContainer<string, int>();

            testee.Extensions.Add(extension);
            testee.Extensions.Clear();

            testee.ForEach(e => executed = true);

            executed
                .Should()
                .BeFalse();
        }
    }
}