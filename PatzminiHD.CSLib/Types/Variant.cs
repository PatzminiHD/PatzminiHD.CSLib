using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Types
{
    //This Variant type is largely copied from vladris (https://github.com/vladris) blog, https://vladris.com/blog/2018/07/16/implementing-a-variant-type-in-csharp.html
    /// <summary>
    /// A Type that is either T1 or T2
    /// </summary>
    /// <typeparam name="T1">Type 1</typeparam>
    /// <typeparam name="T2">Type 2</typeparam>
    public sealed class Variant<T1, T2> : VariantBase
    {
        private Variant(IVariantHolder item, byte index)
            : base(item, index)
        { }

        //T1 constructor and casts
        /// <summary>
        /// Create the Variant as Type T1
        /// </summary>
        /// <param name="item">The value of the variant</param>
        public Variant(T1 item)
            : base(new VariantHolder<T1>(item), 0)
        { }
        /// <summary>
        /// Implicitly cast from Type T1 to variant
        /// </summary>
        /// <param name="item">The value of the variant</param>
        public static implicit operator Variant<T1, T2>(T1 item)
        {
            return new Variant<T1, T2>(item);
        }
        /// <summary>
        /// Explicitly cast from Type T1 to variant
        /// </summary>
        /// <param name="item">The value of the variant</param>
        public static explicit operator T1(Variant<T1, T2> item)
        {
            return item.Get<T1>();
        }
        /// <summary>
        /// Create a new Variant, placing the item explicitly as the first type
        /// </summary>
        /// <param name="item">The value of the variant</param>
        /// <returns></returns>
        public static Variant<T1, T2> Make1(T1 item)
        {
            return new Variant<T1, T2>(new VariantHolder<T1>(item), 0);
        }

        //T2 constructor and casts
        /// <summary>
        /// Create the Variant as Type T2
        /// </summary>
        /// <param name="item">The value of the variant</param>
        public Variant(T2 item)
            : base(new VariantHolder<T2>(item), 1)
        { }
        /// <summary>
        /// Implicitly cast from Type T2 to variant
        /// </summary>
        /// <param name="item">The value of the variant</param>
        public static implicit operator Variant<T1, T2>(T2 item)
        {
            return new Variant<T1, T2>(item);
        }
        /// <summary>
        /// Explicitly cast from Type T2 to variant
        /// </summary>
        /// <param name="item">The value of the variant</param>
        public static explicit operator T2(Variant<T1, T2> item)
        {
            return item.Get<T2>();
        }
        /// <summary>
        /// Create a new Variant, placing the item explicitly as the second type
        /// </summary>
        /// <param name="item">The value of the variant</param>
        /// <returns></returns>
        public static Variant<T1, T2> Make2(T2 item)
        {
            return new Variant<T1, T2>(new VariantHolder<T2>(item), 1);
        }
    }
    /// <summary>
    /// Base class for the variant
    /// </summary>
    public abstract class VariantBase
    {
        private readonly IVariantHolder variant;
        /// <summary>
        /// Index of the Type
        /// </summary>
        public byte Index { get; }
        /// <summary>
        /// Check if the Variant is of type T
        /// </summary>
        /// <typeparam name="T">The type you want to check against</typeparam>
        /// <returns>True if the variant is of type T</returns>
        public bool Is<T>()
        {
            return variant.Is<T>();
        }
        /// <summary>
        /// Get the value of the variant as Type T<br/>
        /// Throws <see cref="InvalidCastException"/> when the Type T is not the type of the variant
        /// </summary>
        /// <typeparam name="T">The type you want the value as</typeparam>
        /// <returns>The value of the Variant as Type T</returns>
        /// <exception cref="InvalidCastException"></exception>
        public T Get<T>()
        {
            return ((VariantHolder<T>)variant).Item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            return variant.Get();
        }

        internal VariantBase(IVariantHolder item, byte index)
        {
            variant = item;
            Index = index;
        }

        //Equality Checks
        /// <summary>
        /// Check if an object is equal to the variant
        /// </summary>
        /// <param name="obj">The object you want to check</param>
        /// <returns>True if the object is equal to the variant</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (VariantBase)obj;

            return Index == other.Index && Get().Equals(other.Get());
        }
        /// <summary>
        /// Get the Hash Code of the Variant
        /// </summary>
        /// <returns>The Hash Code of the Variant</returns>
        public override int GetHashCode()
        {
            return Get().GetHashCode();
        }
    }

    sealed class VariantHolder<T> : IVariantHolder
    {
        public T Item { get; }
        public bool Is<U>()
        {
            return typeof(U) == typeof(T);
        }
        public object Get()
        {
            if (Item == null)
                return new object();
            return Item;
        }
        public VariantHolder(T item)
        {
            Item = item;
        }
    }
    interface IVariantHolder
    {
        bool Is<T>();
        object Get();
    }
}
