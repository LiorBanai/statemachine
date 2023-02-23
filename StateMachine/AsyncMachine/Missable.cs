 
// <copyright file="Missable.cs"  
 

using System;

namespace StateMachine.AsyncMachine
{
    public class Missable<T>
    {
        private T value;

        public Missable()
        {
            this.IsMissing = true;
        }

        public Missable(T value)
        {
            this.Value = value;
        }

        public bool IsMissing { get; private set; }

        public T Value
        {
            get
            {
                if (this.IsMissing)
                {
                    throw new InvalidOperationException("a missing value cannot be accessed.");
                }

                return this.value;
            }

            private set
            {
                this.value = value;

                this.IsMissing = false;
            }
        }
    }
}