 
// <copyright file="InitializableTest.cs"  
 

using System;
using FluentAssertions;
using StateMachine.Infrastructure;
using StateMachine.Machine;
using Xunit;

namespace StateMachine.UnitTests.Infrastructure
{
    public class InitializableTest
    {
        [Fact]
        public void IsInitialized()
        {
            Initializable<SomeClass>
                .Initialized(new SomeClass())
                .IsInitialized
                .Should()
                .BeTrue();

            Initializable<SomeClass>
                .UnInitialized()
                .IsInitialized
                .Should()
                .BeFalse();
        }

        [Fact]
        public void ExtractOr()
        {
            Initializable<string>
                .Initialized("A")
                .ExtractOr("B")
                .Should()
                .Be("A");

            Initializable<string>
                .UnInitialized()
                .ExtractOr("B")
                .Should()
                .Be("B");
        }

        [Fact]
        public void ExtractOrThrow()
        {
            Initializable<string>
                .Initialized("A")
                .ExtractOrThrow()
                .Should()
                .Be("A");

            Initializable<string>
                .UnInitialized()
                .Invoking(x => x.ExtractOrThrow())
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(ExceptionMessages.ValueNotInitialized);
        }

        [Fact]
        public void Map()
        {
            Initializable<SomeClass>
                .Initialized(new SomeClass { SomeValue = "A" })
                .Map(x => x.SomeValue)
                .Should()
                .BeEquivalentTo(Initializable<string>.Initialized("A"));

            Initializable<SomeClass>
                .UnInitialized()
                .Map(x => x.SomeValue)
                .Should()
                .BeEquivalentTo(Initializable<string>.UnInitialized());
        }

        private class SomeClass
        {
            public string SomeValue { get; set; }
        }
    }
}
