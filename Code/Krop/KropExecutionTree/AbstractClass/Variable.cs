// ----------------------------------------------------------------------------
//
// Definition of the Variable class
// Date: June 2018
// Author: S. Gueissaz
//
// ----------------------------------------------------------------------------
using Krop.KropExecutionTree.Interface;

namespace Krop.KropExecutionTree.AbstractClass
{
    /// <summary>
    /// Variable objects
    /// The variable is of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Variable<T> : IVariable
    {
        public abstract string GetName();

        public abstract void SetValue(T _value);

        public abstract T GetValue();

        public abstract override string ToString();
    }
}
