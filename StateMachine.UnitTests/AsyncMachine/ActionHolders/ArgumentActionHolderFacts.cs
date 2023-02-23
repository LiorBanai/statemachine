 
// <copyright file="ArgumentActionHolderFacts.cs"  
 

using System;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using StateMachine.AsyncMachine.ActionHolders;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.ActionHolders
{
    public class ArgumentActionHolderFacts
    {
        [Fact]
        public async Task SyncActionIsInvokedWithSameArgumentThatIsPassedToActionHolderExecuted()
        {
            var expected = new MyArgument();
            MyArgument value = null;
            void SyncAction(MyArgument x) => value = x;

            var testee = new ArgumentActionHolder<MyArgument>(SyncAction);

            await testee.Execute(expected);

            value.Should().Be(expected);
        }

        [Fact]
        public async Task AsyncActionIsInvokedWithSameArgumentThatIsPassedToActionHolderExecuted()
        {
            var expected = new MyArgument();
            MyArgument value = null;
            Task AsyncAction(MyArgument x)
            {
                value = x;
                return Task.CompletedTask;
            }

            var testee = new ArgumentActionHolder<MyArgument>(AsyncAction);

            await testee.Execute(expected);

            value.Should().Be(expected);
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousSyncActionWhenDescribing()
        {
            var testee = new ArgumentActionHolder<MyArgument>(SyncAction);

            var description = testee.Describe();

            description
                .Should()
                .Be("SyncAction");
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousAsyncActionWhenDescribing()
        {
            var testee = new ArgumentActionHolder<MyArgument>(AsyncAction);

            var description = testee.Describe();

            description
                .Should()
                .Be("AsyncAction");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousSyncActionWhenDescribing()
        {
            var testee = new ArgumentActionHolder<MyArgument>(a => { });

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousAsyncActionWhenDescribing()
        {
            var testee = new ArgumentActionHolder<MyArgument>(a => Task.CompletedTask);

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        [Fact]
        public async Task MatchingType()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction);

            await testee.Execute(A.Fake<IBase>());
        }

        [Fact]
        public async Task DerivedType()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction);

            await testee.Execute(A.Fake<IDerived>());
        }

        [Fact]
        public void NonMatchingType()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction);

            Func<Task> action = async () => await testee.Execute(3);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void TooManyArguments()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction);

            Func<Task> action = async () => await testee.Execute(new object[] { 3, 4 });

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void TooFewArguments()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction);

            Func<Task> action = async () => await testee.Execute(new object[] { });

            action.Should().Throw<ArgumentException>();
        }

        private static void SyncAction(MyArgument a)
        {
        }

        private static Task AsyncAction(MyArgument a)
        {
            return Task.CompletedTask;
        }

        private static Task BaseAction(IBase b)
        {
            return Task.CompletedTask;
        }

        private class MyArgument
        {
        }

        public interface IBase
        {
        }

        public interface IDerived : IBase
        {
        }
    }
}