// ----------------------------------------------------------------------------
//
// Definition of the InstructionSetVar class
// Date: June 2018
// Author: S. Gueissaz
//
// ----------------------------------------------------------------------------
using PerCederberg.Grammatica.Runtime;
using Krop.ControlWindow;
using Krop.KropGrammaticaParser;
using Krop.KropExecutionTree.Interface;
using Krop.KropExecutionTree.AbstractClass;
using Krop.KropExecutionTree.Variable;
using System;

namespace Krop.KropExecutionTree.Instruction
{
    /// <summary>
    /// Set variable instruction
    /// </summary>
    class InstructionSetVar : Executable
    {
        Subprogram ParentSubprogram;
        IVariable Var;
        Node SetVarValue;
        bool Error = false;
        string ErrorMsg;

        public InstructionSetVar(Node _nodeIntStatement, Subprogram _parentSubprogram)
        {
            ParentSubprogram = _parentSubprogram;

            for (int i = 0; i < _nodeIntStatement.GetChildCount(); i++)
            {
                switch (_nodeIntStatement.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.WORD:
                        Token token = (Token)_nodeIntStatement.GetChildAt(i);
                        if((Var = ParentSubprogram.GetVar(token.GetImage(), _parentSubprogram)) == null)
                        {
                            Error = true;
                            ErrorMsg = "Variable " + token.GetImage() + " n'existe pas.";
                        }
                        break;
                    case (int)KropConstants.SET_VAR_VALUE:
                        SetVarValue = _nodeIntStatement.GetChildAt(i);
                        break;
                }
            }
        }

        public override bool Execute()
        {
            if (CanExecute())
            {
                if (!Error)
                {
                    if (Var is IntVar intVar)
                    {
                        intVar.SetValue(AlgorithmicExpression.CalculExpression(SetVarValue.GetChildAt(0), ParentSubprogram));
                    }
                    else if (Var is StringVar stringVar)
                    {
                        stringVar.SetValue(AlgorithmicExpression.GetStringValue(SetVarValue.GetChildAt(0), ParentSubprogram));
                    }
                    return true;
                }   
                else
                {
                    FormControlWindow.TerminalWriteLine("VarError : " + ErrorMsg);
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
