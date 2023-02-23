 
// <copyright file="StateDefinitionDictionaryFacts.cs"  
 

using System;
using System.Collections.Generic;
using FluentAssertions;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.States;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine
{
    public class StateDefinitionDictionaryFacts
    {
        [Fact]
        public void ReturnsStateDefinitionIfKeyFound()
        {
            var stateDefinition = new StateDefinition<string, int>("someState");
            var stateDefinitions = new Dictionary<string, IStateDefinition<string, int>>
            {
                { stateDefinition.Id, stateDefinition }
            };
            var testee = new StateDefinitionDictionary<string, int>(stateDefinitions);

            var definition = testee[stateDefinition.Id];

            definition.Should().Be(stateDefinition);
        }

        [Fact]
        public void ThrowsCustomExceptionIfKeyNotFound()
        {
            const string ThisKeyDoesNotExist = "this_key_does_not_exist";

            var emptyDictionary = new Dictionary<string, IStateDefinition<string, int>>();
            var testee = new StateDefinitionDictionary<string, int>(emptyDictionary);

            testee.Invoking(t => t[ThisKeyDoesNotExist])
                .Should().Throw<InvalidOperationException>()
                .WithMessage($"Cannot find StateDefinition for state {ThisKeyDoesNotExist}. Are you sure you have configured this state via myStateDefinitionBuilder.In(..) or myStateDefinitionBuilder.DefineHierarchyOn(..)?");
        }

        [Fact]
        public void ValuesReturnsStateDefinitions()
        {
            var stateDefinitionA = new StateDefinition<string, int>("someState");
            var stateDefinitionB = new StateDefinition<string, int>("someOtherState");
            var stateDefinitionDictionary = new Dictionary<string, IStateDefinition<string, int>>
            {
                { stateDefinitionA.Id, stateDefinitionA },
                { stateDefinitionB.Id, stateDefinitionB }
            };
            var testee = new StateDefinitionDictionary<string, int>(stateDefinitionDictionary);

            var stateDefinitions = testee.Values;

            stateDefinitions
                .Should()
                .HaveCount(2)
                .And.Contain(stateDefinitionA)
                .And.Contain(stateDefinitionB);
        }
    }
}
