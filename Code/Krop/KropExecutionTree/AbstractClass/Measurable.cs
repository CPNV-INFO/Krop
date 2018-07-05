// ----------------------------------------------------------------------------
//
// Definition of the Measurable class
// Date: May 2018
// Author: X. Carrel
// Modified By: S. Gueissaz
//
// ----------------------------------------------------------------------------
using Krop.ControlWindow;

namespace Krop.KropExecutionTree.AbstractClass
{
    /// <summary>
    /// Measurable objects represent expressions that can be computed. 
    /// The result of the computation is of type T
    /// </summary>
    public abstract class Measurable<T>
    {
        /// <summary>
        /// Trigger the computation of the expression
        /// </summary>
        /// <returns> The result of the computation </returns>
        public abstract T Evaluate();

        /// <summary>
        /// Wait ending previous order
        /// </summary>
        /// <returns></returns>
        public bool CanEvaluate()
        {
            while (FormControlWindow.PENDING_INSTRUCTION) //Pausing program execution
            {
            }

            return true;
        }
    }
}
