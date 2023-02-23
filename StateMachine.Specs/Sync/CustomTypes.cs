 
// <copyright file="CustomTypes.cs"  
 

using System;
using FluentAssertions;
using StateMachine.Machine;
using Xbehave;

namespace StateMachine.Specs.Sync
{
    //// see http://www.appccelerate.com/statemachinecustomtypes.html for an explanation why states and events have to be IComparable
    //// and not IEquatable.
    public class CustomTypes
    {
        [Scenario]
        public void CustomTypesForStatesAndEvents(
            PassiveStateMachine<MyState, MyEvent> machine,
            bool arrivedInStateB)
        {
            "establish a state machine with custom types for states and events".x(() =>
            {
                var stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<MyState, MyEvent>();
                stateMachineDefinitionBuilder
                    .In(new MyState("A"))
                        .On(new MyEvent(1)).Goto(new MyState("B"));
                stateMachineDefinitionBuilder
                    .In(new MyState("B"))
                        .ExecuteOnEntry(() => arrivedInStateB = true);
                machine = stateMachineDefinitionBuilder
                    .WithInitialState(new MyState("A"))
                    .Build()
                    .CreatePassiveStateMachine();

                machine.Start();
            });

            "when using the state machine".x(() =>
                machine.Fire(new MyEvent(1)));

            "it should use equals to compare states and events".x(() =>
                arrivedInStateB.Should().BeTrue("state B should be current state"));
        }

        public class MyState : IComparable
        {
            public MyState(string name)
            {
                this.Name = name;
            }

            private string Name { get; }

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

                return this.Equals((MyState)obj);
            }

            public override int GetHashCode()
            {
                return this.Name != null ? this.Name.GetHashCode() : 0;
            }

            public int CompareTo(object obj)
            {
                throw new InvalidOperationException("state machine should not use CompareTo");
            }

            private bool Equals(MyState other)
            {
                return string.Equals(this.Name, other.Name);
            }
        }

        public class MyEvent : IComparable
        {
            public MyEvent(int identifier)
            {
                this.Identifier = identifier;
            }

            private int Identifier { get; }

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

                return this.Equals((MyEvent)obj);
            }

            public override int GetHashCode()
            {
                return this.Identifier;
            }

            public int CompareTo(object obj)
            {
                throw new InvalidOperationException("state machine should not use CompareTo");
            }

            private bool Equals(MyEvent other)
            {
                return this.Identifier == other.Identifier;
            }
        }
    }
}