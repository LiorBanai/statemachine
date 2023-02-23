 
// <copyright file="MissableTest.cs"  
 

using System;
using FluentAssertions;
using StateMachine.Machine;
using Xunit;

namespace StateMachine.UnitTests.Machine
{
    public class MissableTest
    {
        private const string Value = "value";

        [Fact]
        public void ReturnsMissing_WhenNoValueIsSet()
        {
            var testee = new Missable<string>();

            testee.IsMissing.Should().BeTrue();
        }

        [Fact]
        public void ReturnsNotMissing_WhenValueIsSetInConstructor()
        {
            var testee = new Missable<string>(Value);

            testee.IsMissing.Should().BeFalse();
        }

        [Fact]
        public void ReturnsValue_WhenValueIsSetInConstructor()
        {
            var testee = new Missable<string>(Value);

            testee.Value
                .Should().Be(Value);
        }

        [Fact]
        public void ThrowsExceptionOnAccessingValue_WhenValueIsNotSet()
        {
            var testee = new Missable<string>();

            // ReSharper disable once UnusedVariable
            Action action = () => { string v = testee.Value; };

            action.Should().Throw<InvalidOperationException>()
                .WithMessage("*missing*");
        }
    }
}