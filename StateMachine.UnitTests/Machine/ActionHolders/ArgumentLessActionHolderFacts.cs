 
// <copyright file="ArgumentLessActionHolderFacts.cs"  
 

using FluentAssertions;
using StateMachine.Machine.ActionHolders;
using Xunit;

namespace StateMachine.UnitTests.Machine.ActionHolders
{
    public class ArgumentLessActionHolderFacts
    {
        [Fact]
        public void ActionIsInvokedWhenActionHolderIsExecuted()
        {
            var wasExecuted = false;
            void AnAction() => wasExecuted = true;

            var testee = new ArgumentLessActionHolder(AnAction);

            testee.Execute(null);

            wasExecuted
                .Should()
                .BeTrue();
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousActionWhenDescribing()
        {
            var testee = new ArgumentLessActionHolder(Action);

            var description = testee.Describe();

            description
                .Should()
                .Be("Action");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousActionWhenDescribing()
        {
            var testee = new ArgumentLessActionHolder(() => { });

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        private static void Action()
        {
        }
    }
}