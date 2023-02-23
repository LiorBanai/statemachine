 
// <copyright file="ArgumentActionHolderTest.cs"  
 

using System;
using FakeItEasy;
using FluentAssertions;
using StateMachine.Machine.ActionHolders;
using Xunit;

namespace StateMachine.UnitTests.Machine.ActionHolders
{
    public class ArgumentActionHolderTest
    {
        [Fact]
        public void ActionIsInvokedWithSameArgumentThatIsPassedToActionHolderExecuted()
        {
            var expected = new MyArgument();
            MyArgument value = null;
            void AnAction(MyArgument x) => value = x;

            var testee = new ArgumentActionHolder<MyArgument>(AnAction);

            testee.Execute(expected);

            value.Should().Be(expected);
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousActionWhenDescribing()
        {
            var testee = new ArgumentActionHolder<MyArgument>(Action);

            var description = testee.Describe();

            description
                .Should()
                .Be("Action");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousActionWhenDescribing()
        {
            var testee = new ArgumentActionHolder<MyArgument>(a => { });

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        [Fact]
        public void MatchingType()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction);

            testee.Execute(A.Fake<IBase>());
        }

        [Fact]
        public void DerivedType()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction);

            testee.Execute(A.Fake<IDerived>());
        }

        [Fact]
        public void NonMatchingType()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction);

            Action action = () => testee.Execute(3);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void TooManyArguments()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction);

            Action action = () => testee.Execute(new object[] { 3, 4 });

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void TooFewArguments()
        {
            var testee = new ArgumentActionHolder<IBase>(BaseAction);

            Action action = () => testee.Execute(new object[] { });

            action.Should().Throw<ArgumentException>();
        }

        private static void Action(MyArgument a)
        {
        }

        private static void BaseAction(IBase b)
        {
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