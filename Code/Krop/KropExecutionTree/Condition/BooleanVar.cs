// ----------------------------------------------------------------------------
//
// Definition of the BooleanVar class
// Date: May 2018
// Author: X. Carrel
// Modified By: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerCederberg.Grammatica.Runtime;
using PerCederberg.Grammatica.Runtime.RE;
using Krop.KropExecutionTree.AbstractClass;

namespace Krop.KropExecutionTree.Condition
{
    /// <summary>
    /// Holds a Boolean variable
    /// </summary>
    class BooleanVar : Measurable<Boolean>
    {
        private Boolean Value;
        private Boolean IsNot;

        public BooleanVar (Boolean _value, Boolean _isNot)
        {
            Value = _value;
            IsNot = _isNot;
        }

        public override bool Evaluate()
        {
            if (CanEvaluate())
            {
                if (IsNot)
                    return !Value;
                else
                    return Value;
            }
            else
            {
                return false;
            }
        }
    }
}
