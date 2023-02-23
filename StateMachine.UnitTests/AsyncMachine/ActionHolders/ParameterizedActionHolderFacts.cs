 
// <copyright file="ParameterizedActionHolderFacts.cs"  
 

using System.Threading.Tasks;
using FluentAssertions;
using StateMachine.AsyncMachine.ActionHolders;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.ActionHolders
{
    public class ParameterizedActionHolderFacts
    {
        [Fact]
        public async Task SyncActionIsInvokedWithSameArgumentThatIsPassedToConstructor()
        {
            var expected = new MyArgument();
            var wrong = new MyArgument();
            MyArgument value = null;
            void SyncAction(MyArgument x) => value = x;

            var testee = new ParametrizedActionHolder<MyArgument>(SyncAction, expected);

            await testee.Execute(wrong);

            value.Should().Be(expected);
        }

        [Fact]
        public async Task AsyncActionIsInvokedWithSameArgumentThatIsPassedToConstructor()
        {
            var expected = new MyArgument();
            var wrong = new MyArgument();
            MyArgument value = null;
            Task AsyncAction(MyArgument x)
            {
                value = x;
                return Task.CompletedTask;
            }

            var testee = new ParametrizedActionHolder<MyArgument>(AsyncAction, expected);

            await testee.Execute(wrong);

            value.Should().Be(expected);
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousSyncActionWhenDescribing()
        {
            var testee = new ParametrizedActionHolder<MyArgument>(SyncAction, new MyArgument());

            var description = testee.Describe();

            description
                .Should()
                .Be("SyncAction");
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousAsyncActionWhenDescribing()
        {
            var testee = new ParametrizedActionHolder<MyArgument>(AsyncAction, new MyArgument());

            var description = testee.Describe();

            description
                .Should()
                .Be("AsyncAction");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousSyncActionWhenDescribing()
        {
            var testee = new ParametrizedActionHolder<MyArgument>(a => { }, new MyArgument());

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousAsyncActionWhenDescribing()
        {
            var testee = new ParametrizedActionHolder<MyArgument>(a => Task.CompletedTask, new MyArgument());

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        private static void SyncAction(MyArgument a)
        {
        }

        private static Task AsyncAction(MyArgument a)
        {
            return Task.CompletedTask;
        }

        private class MyArgument
        {
        }
    }
}