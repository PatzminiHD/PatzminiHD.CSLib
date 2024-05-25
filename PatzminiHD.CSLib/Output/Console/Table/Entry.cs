using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Output.Console.Table
{
    /// <summary>
    /// Class for a table entry
    /// </summary>
    public class Entry
    {
        private object value;
        private Type type;
        /// <summary> The value of the entry </summary>
        public object Value
        {
            get { return value; }
        }
        /// <summary> The type of the entry </summary>
        public Type Type
        {
            get { return type; }
        }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        //Is disabled so that the ValueWasNull() Method can be used to set the value and type instead of typing the
        //same code every time

        /// <summary> Create a new Entry object with a string type </summary>
        public Entry(string? value)
        {
            if (value != null)
                this.value = value;
            else
                ValueWasNull();
            this.type = typeof(string);
        }
        /// <summary> Create a new Entry object with a int? type </summary>
        public Entry(int? value)
        {
            if (value != null)
            {
                this.value = value;
                this.type = typeof(int);
            }
            else
                ValueWasNull();
        }
        /// <summary> Create a new Entry object with a uint type </summary>
        public Entry(uint? value)
        {
            if (value != null)
            {
                this.value = value;
                this.type = typeof(uint);
            }
            else
                ValueWasNull();
        }
        /// <summary> Create a new Entry object with a double type </summary>
        public Entry(double? value)
        {
            if (value != null)
            {
                this.value = value;
                this.type = typeof(double);
            }
            else
                ValueWasNull();
        }
        /// <summary> Create a new Entry object with a DateTime type </summary>
        public Entry(DateTime? value)
        {
            if (value != null)
            {
                this.value = value;
                this.type = typeof(DateTime);
            }
            else
                ValueWasNull();
        }
        /// <summary> Create a new Entry object with a TimeSpan type </summary>
        public Entry(TimeSpan? value)
        {
            if (value != null)
            {
                this.value = value;
                this.type = typeof(TimeSpan);
            }
            else
                ValueWasNull();
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private void ValueWasNull()
        {
            this.value = "NULL";
            this.type = typeof(string);
        }
    }
}
