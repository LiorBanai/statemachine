using System.Threading.Tasks;
using FluentAssertions;
using StateMachine.AsyncMachine.GuardHolders;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.GuardHolders
{
    public class ArgumentLessGuardHolderFacts
    {
        [Fact]
        public async Task SyncActionIsInvokedWhenGuardHolderIsExecuted()
        {
            var wasExecuted = false;
            bool SyncGuard()
            {
                wasExecuted = true;
                return true;
            }

            var testee = new ArgumentLessGuardHolder(SyncGuard);

            await testee.Execute(null);

            wasExecuted
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task AsyncActionIsInvokedWheGuardHolderIsExecuted()
        {
            var wasExecuted = false;
            Task<bool> SyncGuard()
            {
                wasExecuted = true;
                return Task.FromResult(true);
            }

            var testee = new ArgumentLessGuardHolder(SyncGuard);

            await testee.Execute(null);

            wasExecuted
                .Should()
                .BeTrue();
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousSyncActionWhenDescribing()
        {
            var testee = new ArgumentLessGuardHolder(SyncGuard);

            var description = testee.Describe();

            description
                .Should()
                .Be("SyncGuard");
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousAsyncActionWhenDescribing()
        {
            var testee = new ArgumentLessGuardHolder(AsyncGuard);

            var description = testee.Describe();

            description
                .Should()
                .Be("AsyncGuard");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousSyncActionWhenDescribing()
        {
            var testee = new ArgumentLessGuardHolder(() => true);

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousAsyncActionWhenDescribing()
        {
            var testee = new ArgumentLessGuardHolder(() => Task.FromResult(true));

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        private static bool SyncGuard()
        {
            return true;
        }

        private static Task<bool> AsyncGuard()
        {
            return Task.FromResult(true);
        }
    }
}