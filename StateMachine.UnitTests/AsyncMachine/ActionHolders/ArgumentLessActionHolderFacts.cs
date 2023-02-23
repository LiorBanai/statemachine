 
// <copyright file="ArgumentLessActionHolderFacts.cs"  
 

using System.Threading.Tasks;
using FluentAssertions;
using StateMachine.AsyncMachine.ActionHolders;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.ActionHolders
{
    public class ArgumentLessActionHolderFacts
    {
        [Fact]
        public async Task SyncActionIsInvokedWhenActionHolderIsExecuted()
        {
            var wasExecuted = false;
            void SyncAction() => wasExecuted = true;

            var testee = new ArgumentLessActionHolder(SyncAction);

            await testee.Execute(null);

            wasExecuted
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task AsyncActionIsInvokedWhenActionHolderIsExecuted()
        {
            var wasExecuted = false;
            Task AsyncAction()
            {
                wasExecuted = true;
                return Task.CompletedTask;
            }

            var testee = new ArgumentLessActionHolder(AsyncAction);

            await testee.Execute(null);

            wasExecuted
                .Should()
                .BeTrue();
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousSyncActionWhenDescribing()
        {
            var testee = new ArgumentLessActionHolder(SyncAction);

            var description = testee.Describe();

            description
                .Should()
                .Be("SyncAction");
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousAsyncActionWhenDescribing()
        {
            var testee = new ArgumentLessActionHolder(AsyncAction);

            var description = testee.Describe();

            description
                .Should()
                .Be("AsyncAction");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousSyncActionWhenDescribing()
        {
            var testee = new ArgumentLessActionHolder(() => { });

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousAsyncActionWhenDescribing()
        {
            var testee = new ArgumentLessActionHolder(() => Task.CompletedTask);

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        private static void SyncAction()
        {
        }

        private static Task AsyncAction()
        {
            return Task.CompletedTask;
        }
    }
}