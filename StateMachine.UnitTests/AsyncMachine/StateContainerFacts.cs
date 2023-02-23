 
// <copyright file="StateContainerFacts.cs"  
 

using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using StateMachine.AsyncMachine;
using StateMachine.Infrastructure;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine
{
    public class StateContainerFacts
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
        public async Task ExtensionsWhenExtensionsAreClearedThenNoExtensionIsRegistered()
        {
            var executed = false;
            var extension = A.Fake<IExtensionInternal<string, int>>();

            var testee = new StateContainer<string, int>();

            testee.Extensions.Add(extension);
            testee.Extensions.Clear();

            await testee.ForEach(e =>
                {
                    executed = true;
                    return Task.CompletedTask;
                })
                .ConfigureAwait(false);

            executed
                .Should().BeFalse();
        }
    }
}