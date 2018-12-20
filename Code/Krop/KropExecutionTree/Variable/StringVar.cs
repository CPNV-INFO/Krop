// ----------------------------------------------------------------------------
//
// Definition of the StringVar class
// Date: November 2018
// Author: Dorian Niclass
//
// ----------------------------------------------------------------------------
using Krop.KropExecutionTree.AbstractClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krop.KropExecutionTree.Variable
{
    class StringVar : Variable<string>
    {
        private string Name;
        private string Value;

        public StringVar(string _name, string _value)
        {
            this.Name = _name;
            this.Value = _value;
        }

        public override string GetName()
        {
            return this.Name;
        }

        public override string GetValue()
        {
            return this.Value;
        }

        public override void SetValue(string _value)
        {
            Value = _value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
