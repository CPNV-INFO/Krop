// ----------------------------------------------------------------------------
//
// Definition of the AlgorithmicExpression class
// Date: June 2018
// Author: S. Gueissaz
//
// ----------------------------------------------------------------------------
using System;
using PerCederberg.Grammatica.Runtime;
using Krop.KropGrammaticaParser;

namespace Krop.KropExecutionTree
{
    /// <summary>
    /// Contain all Algorithmic Calculation functions
    /// </summary>
    public static class AlgorithmicExpression
    {
        /// <summary>
        /// Calculate an algorithmic expression
        /// </summary>
        /// <param name="_nodeExpression">Node containing the algorithmic expression</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Result of the calculation</returns>
        public static int? CalculExpression(Node _nodeExpression, Subprogram _parentSubprogram)
        {
            int? value = 0;

            for (int i = 0; i < _nodeExpression.GetChildCount(); i++)
            {
                switch (_nodeExpression.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.TERM:
                        Console.WriteLine(_nodeExpression.GetParent().GetChildAt(0));
                        if (_nodeExpression.GetParent().GetChildAt(0).GetId() == (int)KropConstants.SUB)
                            value -= CalculTerm(_nodeExpression.GetChildAt(i), _parentSubprogram);
                        else
                            value += CalculTerm(_nodeExpression.GetChildAt(i), _parentSubprogram);
                        break;
                    case (int)KropConstants.EXPRESSION_REST:
                        value += CalculExpressionRest(_nodeExpression.GetChildAt(i), _parentSubprogram);
                        break;
                }
            }
            return value;
        }

        /// <summary>
        /// Calculate the term of an algorithmic expression
        /// </summary>
        /// <param name="_nodeTerm">Node containing the term</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Result of the calculation</returns>
        private static int? CalculTerm(Node _nodeTerm, Subprogram _parentSubprogram)
        {
            int? value = 0;

            for (int i = 0; i < _nodeTerm.GetChildCount(); i++)
            {
                switch (_nodeTerm.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.FACTOR:
                        value = CalculFactor(_nodeTerm.GetChildAt(i), _parentSubprogram);
                        break;
                    case (int)KropConstants.TERM_REST:
                        value = CalculTermRest(value, _nodeTerm.GetChildAt(i), _parentSubprogram);
                        break;
                }
            }
            
            return value;
        }

        /// <summary>
        /// Calculate the termRest of a term
        /// </summary>
        /// <param name="_value">Value of the term</param>
        /// <param name="_nodeTermRest">Node containing the termRest</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Result of the calculation</returns>
        private static int? CalculTermRest(int? _value, Node _nodeTermRest, Subprogram _parentSubprogram)
        {
            int? value = _value;

            for (int i = 0; i < _nodeTermRest.GetChildCount(); i++)
            {
                if(_nodeTermRest.GetChildAt(i).GetId() == (int)KropConstants.TERM)
                {
                    for (int y = 0; y < _nodeTermRest.GetChildAt(i).GetChildCount(); y++)
                    {
                        switch (_nodeTermRest.GetChildAt(i).GetChildAt(y).GetId())
                        {
                            case (int)KropConstants.FACTOR:
                                if (_nodeTermRest.GetChildAt(0).GetId() == (int)KropConstants.MUL)
                                {
                                    value *= CalculFactor(_nodeTermRest.GetChildAt(i).GetChildAt(y), _parentSubprogram);
                                }
                                else
                                {
                                    value /= CalculFactor(_nodeTermRest.GetChildAt(i).GetChildAt(y), _parentSubprogram);
                                }
                                break;
                            case (int)KropConstants.TERM_REST:
                                value = CalculTermRest(value, _nodeTermRest.GetChildAt(i).GetChildAt(y), _parentSubprogram);
                                break;
                        }
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Return the value of the term factor
        /// </summary>
        /// <param name="_nodeFactor">Node containing the factor</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Factor value</returns>
        private static int? CalculFactor(Node _nodeFactor, Subprogram _parentSubprogram)
        {
            int? value = 0;

            for (int i = 0; i < _nodeFactor.GetChildCount(); i++)
            {
                switch (_nodeFactor.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.ATOM:
                        value = CalcultAtom(_nodeFactor.GetChildAt(i), _parentSubprogram);
                        break;
                    case (int)KropConstants.EXPRESSION:
                        value = CalculExpression(_nodeFactor.GetChildAt(i), _parentSubprogram);
                        break;
                }
            }

            return value;
        }

        /// <summary>
        /// Return the value of the factor atom
        /// </summary>
        /// <param name="_nodeAtom">Node containing the atom</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Atom value</returns>
        private static int? CalcultAtom(Node _nodeAtom, Subprogram _parentSubprogram)
        {
            Token token = (Token)_nodeAtom.GetChildAt(0);
            int value;

            switch (_nodeAtom.GetChildAt(0).GetId())
            {
                case (int)KropConstants.SUB:
                    token = (Token)_nodeAtom.GetChildAt(1);
                    Int32.TryParse(token.GetImage(), out value);
                    return -value;
                case (int)KropConstants.NUMBER:
                    Int32.TryParse(token.GetImage(), out value);
                    return value;
                case (int)KropConstants.WORD:
                    return Subprogram.GetIntVarValue(token.GetImage(), _parentSubprogram);
                default:
                    return null;
            }


        }

        /// <summary>
        /// Calculate the expressionRest of an expression
        /// </summary>
        /// <param name="_nodeExpressionRest">Node containing the expressionRest</param>
        /// <param name="_parentSubprogram">Parent Subprogram</param>
        /// <returns>Result of the calculation</returns>
        private static int? CalculExpressionRest(Node _nodeExpressionRest, Subprogram _parentSubprogram)
        {
            int? value = 0;

            for (int i = 0; i < _nodeExpressionRest.GetChildCount(); i++)
            {
                switch (_nodeExpressionRest.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.EXPRESSION:
                        value = CalculExpression(_nodeExpressionRest.GetChildAt(i), _parentSubprogram);
                        break;
                }
            }

            return value;
        }
    }
}
