 
// <copyright file="TransitionFactsBase.cs"  
 

using System;
using System.Threading.Tasks;
using FakeItEasy;
using StateMachine.AsyncMachine;
using StateMachine.AsyncMachine.States;
using StateMachine.AsyncMachine.Transitions;

namespace StateMachine.UnitTests.AsyncMachine.Transitions
{
    public class TransitionFactsBase
    {
        public enum States
        {
        }

        public enum Events
        {
        }

        protected TransitionDefinition<States, Events> TransitionDefinition { get; }

        protected TransitionLogic<States, Events> Testee { get; }

        protected IStateLogic<States, Events> StateLogic { get; }

        protected ILastActiveStateModifier<States> LastActiveStateModifier { get; }

        protected TestableExtensionHost ExtensionHost { get; }

        protected IStateDefinitionDictionary<States, Events> StateDefinitions { get; }

        protected IStateDefinition<States, Events> Source { get; set; }

        protected IStateDefinition<States, Events> Target { get; set; }

        protected ITransitionContext<States, Events> TransitionContext { get; set; }

        protected TransitionFactsBase()
        {
            this.StateLogic = A.Fake<IStateLogic<States, Events>>();
            this.LastActiveStateModifier = A.Fake<ILastActiveStateModifier<States>>();
            this.StateDefinitions = A.Fake<IStateDefinitionDictionary<States, Events>>();
            this.ExtensionHost = new TestableExtensionHost();
            this.TransitionDefinition = new TransitionDefinition<States, Events>();

            this.Testee = new TransitionLogic<States, Events>(this.ExtensionHost);
            this.Testee.SetStateLogic(this.StateLogic);
        }

        protected class TestableExtensionHost : IExtensionHost<States, Events>
        {
            public IExtensionInternal<States, Events> Extension { private get; set; }

            public async Task ForEach(Func<IExtensionInternal<States, Events>, Task> action)
            {
                if (this.Extension != null)
                {
                    await action(this.Extension);
                }
            }
        }
    }
}