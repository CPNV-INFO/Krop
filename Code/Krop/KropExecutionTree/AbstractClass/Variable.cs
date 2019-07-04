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

        public virtual void SetValue(T _value)
        {
            Subprogram.VarValueChanged(this);
        }

        public abstract T GetValue();

        public abstract override string ToString();
    }
}
