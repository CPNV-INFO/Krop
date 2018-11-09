using Krop.ControlWindow;
using Krop.KropExecutionTree.AbstractClass;
using Krop.KropGrammaticaParser;
using PerCederberg.Grammatica.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krop.KropExecutionTree.String
{
    class StringExpression : Measurable<string>
    {
        Subprogram ParentSubprogram;
        Node ExpressionOne;
        Node ExpressionTwo;
        bool add = false;

        public StringExpression(Node _nodeStringExpression, Subprogram _parentSubprogram)
        {
            ParentSubprogram = _parentSubprogram;

            for (int i = 0; i < _nodeStringExpression.GetChildCount(); i++)
            {
                switch (_nodeStringExpression.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.EXPRESSION:
                        ExpressionOne = _nodeStringExpression.GetChildAt(i);
                        break;
                    case (int)KropConstants.BOOLEAN_EXPRESSION_REST:
                        for (int y = 0; y < _nodeStringExpression.GetChildAt(i).GetChildCount(); y++)
                        {
                            switch (_nodeStringExpression.GetChildAt(i).GetChildAt(y).GetId())
                            {
                                case (int)KropConstants.ADD:
                                    add = true;
                                    break;
                                case (int)KropConstants.STRING_VALUE:
                                    ExpressionTwo = _nodeStringExpression.GetChildAt(i).GetChildAt(y);
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        public override string Evaluate()
        {
            string valueOne = ExpressionOne.ToString();
            string valueTwo = ExpressionTwo.ToString();
            string result = "";

            if (CanEvaluate())
            {
                if (valueOne != null && valueTwo != null)
                {
                    if (add)
                    {
                        result = valueOne + valueTwo;
                        return result;
                    }
                    else
                    {
                        return "";
                    }

                }
                else
                {
                   
                    string errorMsg = valueOne + " + " + valueTwo;
                    

                    FormControlWindow.TerminalWriteLine("La condition ( " + errorMsg + " ) est impossible.");
                    return "";
                }


            }
            else
            {
                return "";
            }
        }
    }
}
