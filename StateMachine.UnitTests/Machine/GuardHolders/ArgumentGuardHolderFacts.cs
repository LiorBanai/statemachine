 
// <copyright file="ArgumentGuardHolderFacts.cs"  
 

using System;
using FakeItEasy;
using FluentAssertions;
using StateMachine.Machine.GuardHolders;
using Xunit;

namespace StateMachine.UnitTests.Machine.GuardHolders
{
    public class ArgumentGuardHolderFacts
    {
        [Fact]
        public void ActionIsInvokedWithSameArgumentThatIsPassedToGuardHolderExecuted()
        {
            var expected = new MyArgument();
            MyArgument value = null;
            bool Guard(MyArgument x)
            {
                value = x;
                return true;
            }

            var testee = new ArgumentGuardHolder<MyArgument>(Guard);

            testee.Execute(expected);

            value.Should().Be(expected);
        }

        [Fact]
        public void ReturnsFunctionNameForNonAnonymousActionWhenDescribing()
        {
            var testee = new ArgumentGuardHolder<MyArgument>(Guard);

            var description = testee.Describe();

            description
                .Should()
                .Be("Guard");
        }

        [Fact]
        public void ReturnsAnonymousForAnonymousActionWhenDescribing()
        {
            var testee = new ArgumentGuardHolder<MyArgument>(a => true);

            var description = testee.Describe();

            description
                .Should()
                .Be("anonymous");
        }

        [Fact]
        public void MatchingType()
        {
            var testee = new ArgumentGuardHolder<IBase>(BaseGuard);

            testee.Execute(A.Fake<IBase>());
        }

        [Fact]
        public void DerivedType()
        {
            var testee = new ArgumentGuardHolder<IBase>(BaseGuard);

            testee.Execute(A.Fake<IDerived>());
        }

        [Fact]
        public void NonMatchingType()
        {
            var testee = new ArgumentGuardHolder<IBase>(BaseGuard);

            Action action = () => testee.Execute(3);

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage(GuardHoldersExceptionMessages.CannotCastArgumentToGuardArgument(3, "BaseGuard"));
        }

        private static bool Guard(MyArgument a)
        {
            return true;
        }

        private static bool BaseGuard(IBase b)
        {
            return true;
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