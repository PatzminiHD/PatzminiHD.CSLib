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


        /// <summary> Create a new Entry object with a string type </summary>
        public Entry(string value)
        {
            this.value = value;
            this.type = typeof(string);
        }
        /// <summary> Create a new Entry object with a int type </summary>
        public Entry(int value)
        {
            this.value = value;
            this.type = typeof(int);
        }
        /// <summary> Create a new Entry object with a double type </summary>
        public Entry(double value)
        {
            this.value = value;
            this.type = typeof(double);
        }
        /// <summary> Create a new Entry object with a DateTime type </summary>
        public Entry(DateTime value)
        {
            this.value = value;
            this.type = typeof(DateTime);
        }
    }
}
