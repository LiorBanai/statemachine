﻿ 
// <copyright file="SuccessfulTransitionWithExecutedActionsFactsBase.cs"  
 

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.ActionHolders;
using StateMachine.AsyncMachine.States;
using StateMachine.AsyncMachine.Transitions;
using Xunit;

namespace StateMachine.UnitTests.AsyncMachine.Transitions
{
    public abstract class SuccessfulTransitionWithExecutedActionsFactsBase : TransitionFactsBase
    {
        [Fact]
        public async Task ReturnsSuccessfulTransitionResult()
        {
            var result = await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            result.Should().BeSuccessfulTransitionResultWithNewState(this.Target);
        }

        [Fact]
        public async Task ExecutesActions()
        {
            var executed = false;

            this.TransitionDefinition.ActionsModifiable.Add(new ArgumentLessActionHolder(() => executed = true));

            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            executed.Should().BeTrue("actions should be executed");
        }

        [Fact]
        public async Task TellsExtensionsAboutExecutedTransition()
        {
            var extension = new FakeExtension();
            this.ExtensionHost.Extension = extension;

            await this.Testee.Fire(this.TransitionDefinition, this.TransitionContext, this.LastActiveStateModifier, this.StateDefinitions);

            extension.Items.Should().Contain(new FakeExtension.Item(
                this.Source,
                this.Target,
                this.TransitionContext));
        }

        public class FakeExtension : InternalExtensionBase<States, Events>
        {
            private readonly List<Item> items = new List<Item>();

            public override Task ExecutedTransition(
                ITransitionDefinition<States, Events> transition,
                ITransitionContext<States, Events> transitionContext)
            {
                this.items.Add(new Item(transition.Source, transition.Target, transitionContext));

                return Task.CompletedTask;
            }

            public IReadOnlyCollection<Item> Items => this.items;

            public class Item
            {
                private bool Equals(Item other)
                {
                    return
                        Equals(this.Source, other.Source) &&
                        (Equals(this.Target, other.Target) || (this.Target == null && other.Target == other.Source)) && // in case of an internal-transition, this.Target (from TransitionContext) is null (wherease it would be == this.Source in case of an self-transition) therefor we check the we did not switch state in this case
                        Equals(this.TransitionContext, other.TransitionContext);
                }

                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj))
                    {
                        return false;
                    }

                    if (ReferenceEquals(this, obj))
                    {
                        return true;
                    }

                    if (obj.GetType() != this.GetType())
                    {
                        return false;
                    }

                    return this.Equals((Item)obj);
                }

                public override int GetHashCode()
                {
                    unchecked
                    {
                        var hashCode = this.Source != null ? this.Source.GetHashCode() : 0;
                        hashCode = (hashCode * 397) ^ (this.Target != null ? this.Target.GetHashCode() : 0);
                        hashCode = (hashCode * 397) ^ (this.TransitionContext != null ? this.TransitionContext.GetHashCode() : 0);
                        return hashCode;
                    }
                }

                public Item(
                    IStateDefinition<States, Events> source,
                    IStateDefinition<States, Events> target,
                    ITransitionContext<States, Events> transitionContext)
                {
                    this.Source = source;
                    this.Target = target;
                    this.TransitionContext = transitionContext;
                }

                public IStateDefinition<States, Events> Source { get; }

                public IStateDefinition<States, Events> Target { get; }

                public ITransitionContext<States, Events> TransitionContext { get; }
            }
        }
    }
}