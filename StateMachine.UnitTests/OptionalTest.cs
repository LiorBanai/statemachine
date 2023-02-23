 
// <copyright file="OptionalTest.cs"  
 

using FluentAssertions;
using StateMachine.Infrastructure;
using Xunit;

namespace StateMachine.UnitTests
{
    public class OptionalTest
    {
        [Fact]
        public void IsInitialized()
        {
            Optional<string>
                .Just("hello world")
                .HasValue
                .Should()
                .BeTrue();

            Optional<string>
                .Nothing()
                .HasValue
                .Should()
                .BeFalse();
        }
    }
}
