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
using System.Windows.Forms;
using System.Linq;

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
        Token varToken;
        bool inputValue = false;
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
                        varToken = (Token)_nodeIntStatement.GetChildAt(i);                  
                        break;
                    case (int)KropConstants.SET_VAR_VALUE:
                        if (_nodeIntStatement.GetChildAt(i).GetChildAt(0).GetId() == (int)KropConstants.INPUT)
                            inputValue = true;       
                        else
                            SetVarValue = _nodeIntStatement.GetChildAt(i);
                        break;
                }
            }
        }

        public override bool Execute()
        {
            if (CanExecute())
            {
                if ((Var = ParentSubprogram.GetVar(varToken.GetImage(), ParentSubprogram)) == null)
                {
                    Error = true;
                    ErrorMsg = "Variable " + varToken.GetImage() + " n'existe pas.";
                }

                if (!Error)
                {
                    if (Var is IntVar intVar) {

                        if (inputValue)
                        {
                            intVar.SetValue(Subprogram.PromptInputValue<int>());
                        }
                        else
                        {
                            intVar.SetValue(AlgorithmicExpression.CalculExpression(SetVarValue.GetChildAt(0), ParentSubprogram));
                        }
                    }
                    else if (Var is StringVar stringVar)
                    {
                        if (inputValue)
                        {
                            stringVar.SetValue(Subprogram.PromptInputValue<string>());
                        }
                        else
                        {
                            stringVar.SetValue(AlgorithmicExpression.CalculStringExpression(SetVarValue.GetChildAt(0), ParentSubprogram));
                        }
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
