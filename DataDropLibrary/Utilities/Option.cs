using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDropLibrary.Utilities
{
    public abstract class Option<T>
    {
        public static implicit operator Option<T>(T value) => new Some<T>(value);
        public static implicit operator Option<T>(None none) => new None<T>();
    }

    public sealed class Some<T> : Option<T>
    {
        public T Content { get; }
        public Some(T value) => Content = value;
    }

    public sealed class None<T> : Option<T>
    {
    }

    public sealed class None
    {
        public static None Value { get; } = new None();
        private None() { }
    }
}
