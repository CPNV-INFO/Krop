// ----------------------------------------------------------------------------
//
// Definition of the IntVar class
// Date: June 2018
// Author: S. Gueissaz
//
// ----------------------------------------------------------------------------
using Krop.KropExecutionTree.AbstractClass;

namespace Krop.KropExecutionTree.Variable
{
    /// <summary>
    /// Hold an Int variable
    /// </summary>
    class IntVar : Variable<int?>
    {
        private string Name;
        private int? Value;

        public IntVar(string _name, int? _value)
        {
            Name = _name;
            Value = _value;
        }

        /// <summary>
        /// Return variable name
        /// </summary>
        /// <returns>Variable name</returns>
        public override string GetName()
        {
            return Name;
        }

        /// <summary>
        /// Return variable value
        /// </summary>
        /// <returns>Variable value</returns>
        public override int? GetValue()
        {
            return Value;
        }

        /// <summary>
        /// Set variable value
        /// </summary>
        /// <param name="_value">Value</param>
        public override void SetValue(int? _value)
        {
            Value = _value;

            base.SetValue(_value);
        }

        /// <summary>
        /// Return a string of the variable
        /// </summary>
        /// <returns>String variable</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
