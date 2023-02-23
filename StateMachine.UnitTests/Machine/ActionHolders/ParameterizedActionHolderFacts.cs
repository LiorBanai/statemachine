 
// <copyright file="ParameterizedActionHolderFacts.cs"  
 

using FluentAssertions;
using StateMachine.Machine.ActionHolders;
using Xunit;

namespace StateMachine.UnitTests.Machine.ActionHolders
{
    public class ParameterizedActionHolderFacts
    {
        [Fact]
        public void ActionIsInvokedWithSameArgumentThatIsPassedToConstructor()
        {
            var expected = new MyArgument();
            var wrong = new MyArgument();
            MyArgument value = null;
            void AnAction(MyArgument x) => value = x;

            var testee = new ParametrizedActionHolder<MyArgument>(AnAction, expected);

            testee.Execute(wrong);

            value.Should().Be(expected);
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousActionWhenDescribing()
        {
            var testee = new ParametrizedActionHolder<MyArgument>(Action, new MyArgument());

            var description = testee.Describe();

            description
                .Should()
                .Be("Action");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousActionWhenDescribing()
        {
            var testee = new ParametrizedActionHolder<MyArgument>(a => { }, new MyArgument());

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        private static void Action(MyArgument a)
        {
        }

        private class MyArgument
        {
        }
    }
}