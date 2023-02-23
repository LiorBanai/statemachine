 
// <copyright file="Optional.cs"  
 

namespace StateMachine.Infrastructure
{
    public class Optional<T>
    {
        private Optional()
        {
        }

        public T Value { get; private set; }

        public bool HasValue { get; private set; }

        public static Optional<T> Just(T value)
        {
            return new Optional<T>
            {
                HasValue = true,
                Value = value
            };
        }

        public static Optional<T> Nothing()
        {
            return new Optional<T>();
        }
    }
}
