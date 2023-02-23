

using FluentAssertions;
using StateMachine.Machine.GuardHolders;
using Xunit;

namespace StateMachine.UnitTests.Machine.GuardHolders
{
    public class ArgumentLessGuardHolderFacts
    {
        [Fact]
        public void ActionIsInvokedWhenGuardHolderIsExecuted()
        {
            var wasExecuted = false;
            bool Guard()
            {
                wasExecuted = true;
                return true;
            }

            var testee = new ArgumentLessGuardHolder(Guard);

            testee.Execute(null);

            wasExecuted
                .Should()
                .BeTrue();
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousActionWhenDescribing()
        {
            var testee = new ArgumentLessGuardHolder(Guard);

            var description = testee.Describe();

            description
                .Should()
                .Be("Guard");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousActionWhenDescribing()
        {
            var testee = new ArgumentLessGuardHolder(() => true);

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        private static bool Guard()
        {
            return true;
        }
    }
}