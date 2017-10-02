using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Option
{
    public class Option<T>
    {
        readonly static Option<T> NONE_OBJECT = new Option<T>();
        readonly T value;

        private Option() { }

        private Option(T value)
        {
            this.value = value;
        }

        public static Option<T> Some(T value) => new Option<T>(value);

        public static Option<T> None() => NONE_OBJECT;

        public bool IsSome() => this != NONE_OBJECT;

        public bool IsNone() => this == NONE_OBJECT;

        public T Value()
        {
            if (IsNone())
            {
                throw new Exception("There is no value in None");
            }

            return value;
        }

        public Option<U> Map<U>(Func<T, U> f)
        {
            if (IsNone())
            {
                return Option<U>.None();
            }

            return Option<U>.Some(f(value));
        }

        public static Option<T> Flatten(Option<Option<T>> option)
        {
            if (option.IsNone())
            {
                return Option<T>.None();
            }

            return option.value;
        }
    }
}
