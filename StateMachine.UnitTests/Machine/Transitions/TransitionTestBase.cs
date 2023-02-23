 
// <copyright file="TransitionTestBase.cs"  
 

using System;
using FakeItEasy;
using StateMachine.Machine;
using StateMachine.Machine.States;
using StateMachine.Machine.Transitions;

namespace StateMachine.UnitTests.Machine.Transitions
{
    public class TransitionTestBase
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

        public TransitionTestBase()
        {
            this.StateLogic = A.Fake<IStateLogic<States, Events>>();
            this.LastActiveStateModifier = A.Fake<ILastActiveStateModifier<States>>();
            this.StateDefinitions = A.Fake<IStateDefinitionDictionary<States, Events>>();
            this.ExtensionHost = new TestableExtensionHost();
            this.TransitionDefinition = new TransitionDefinition<States, Events>();

            this.Testee = new TransitionLogic<States, Events>(this.ExtensionHost);
            this.Testee.SetStateLogic(this.StateLogic);
        }

        public class TestableExtensionHost : IExtensionHost<States, Events>
        {
            public IExtensionInternal<States, Events> Extension { private get; set; }

            public void ForEach(Action<IExtensionInternal<States, Events>> action)
            {
                if (this.Extension != null)
                {
                    action(this.Extension);
                }
            }
        }
    }
}