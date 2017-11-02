namespace Option
{
    using System;
    using System.Collections.Generic;

    public class Option<T>
    {
        private readonly static Option<T> _NONE_OBJECT;
        private readonly T _value;

        static Option()
        {
            _NONE_OBJECT = new Option<T>(default(T));
        }

        private Option(T value)
        {
            this._value = value;
        }

        public static Option<T> Some(T value) => new Option<T>(value);

        public static Option<T> None => _NONE_OBJECT;

        public bool IsSome => this != _NONE_OBJECT;

        public bool IsNone => this == _NONE_OBJECT;

        public T Value
        {
            get
            {
                if (IsNone)
                {
                    throw new Exception("There is no value in None");
                }

                return _value;
            }
        }

        public Option<U> Map<U>(Func<T, U> f)
        {
            if (IsNone)
            {
                return Option<U>.None;
            }

            return Option<U>.Some(f(_value));
        }

        public override bool Equals(object o)
        {
            if (!(o is Option<T>))
            {
                return false;
            }

            Option<T> option = o as Option<T>;

            if (IsNone != option.IsNone)
            {
                return false;
            }

            if (IsNone)
            {
                return true;
            }

            return _value.Equals(option._value);
        }

        public static Option<T> Flatten(Option<Option<T>> option)
        {
            if (option.IsNone)
            {
                return Option<T>.None;
            }

            return option._value;
        }

        public override int GetHashCode()
        {
            var hashCode = 2139456307;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(_value);
            hashCode = hashCode * -1521134295 + IsSome.GetHashCode();
            hashCode = hashCode * -1521134295 + IsNone.GetHashCode();
            return hashCode;
        }
    }
}
