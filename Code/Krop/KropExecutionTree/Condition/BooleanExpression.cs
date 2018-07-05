using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerCederberg.Grammatica.Runtime;
using PerCederberg.Grammatica.Runtime.RE;
using Krop.ControlWindow;
using Krop.KropGrammaticaParser;
using Krop.KropExecutionTree.AbstractClass;

namespace Krop.KropExecutionTree.Condition
{
    /// <summary>
    /// Holds a Boolean expression
    /// </summary>
    class BooleanExpression : Measurable<Boolean>
    {
        Subprogram ParentSubprogram;
        Node ExpressionOne;
        Node ExpressionTwo;
        bool IsNot = false;
        bool IsEgal = false;
        bool IsBigger = false;
        bool IsSmaller = false;

        public BooleanExpression(Node _nodeBooleanExpression, Subprogram _parentSubprogram, bool _isNot)
        {
            ParentSubprogram = _parentSubprogram;
            IsNot = _isNot;

            for(int i = 0; i < _nodeBooleanExpression.GetChildCount(); i++)
            {
                switch (_nodeBooleanExpression.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.EXPRESSION:
                        ExpressionOne = _nodeBooleanExpression.GetChildAt(i);
                        break;
                    case (int)KropConstants.BOOLEAN_EXPRESSION_REST:
                        for(int y = 0; y < _nodeBooleanExpression.GetChildAt(i).GetChildCount(); y++)
                        {
                            switch (_nodeBooleanExpression.GetChildAt(i).GetChildAt(y).GetId())
                            {
                                case (int)KropConstants.EGAL:
                                    IsEgal = true;
                                    break;
                                case (int)KropConstants.BIGGER:
                                    IsBigger = true;
                                    break;
                                case (int)KropConstants.SMALLER:
                                    IsSmaller = true;
                                    break;
                                case (int)KropConstants.EXPRESSION:
                                    ExpressionTwo = _nodeBooleanExpression.GetChildAt(i).GetChildAt(y);
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        public override bool Evaluate()
        {
            int? valueOne = AlgorithmicExpression.CalculExpression(ExpressionOne, ParentSubprogram);
            int? valueTwo = AlgorithmicExpression.CalculExpression(ExpressionTwo, ParentSubprogram);
            bool result = false;
            string errorMsg;

            if (CanEvaluate())
            {
                if(valueOne != null && valueTwo != null)
                {
                    if (IsEgal)
                    {
                        result = valueOne == valueTwo;
                    }
                    else if (IsBigger)
                    {
                        result = valueOne > valueTwo;
                    }
                    else if(IsSmaller)
                    {
                        result = valueOne < valueTwo;
                    }

                    if (IsNot)
                        return !result;
                    else
                        return result;
                }
                else
                {
                    if (IsEgal)
                    {
                        errorMsg = valueOne + " = " + valueTwo;
                    }
                    else if (IsBigger)
                    {
                        errorMsg = valueOne + " > " + valueTwo;
                    }
                    else
                    {
                        errorMsg = valueOne + " < " + valueTwo;
                    }

                    FormControlWindow.TerminalWriteLine("La condition ( " + errorMsg + " ) est impossible.");
                    return false;
                }

                
            }
            else
            {
                return false;
            }
        }
    }
}
