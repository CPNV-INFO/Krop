﻿// ----------------------------------------------------------------------------
//
// Definition of the InstructionString class
// Date: 03.11.2018
// Author: Dorian Niclass
//
// ----------------------------------------------------------------------------
using PerCederberg.Grammatica.Runtime;
using Krop.ControlWindow;
using Krop.KropGrammaticaParser;
using Krop.KropExecutionTree.AbstractClass;
using Krop.KropExecutionTree.Variable;

namespace Krop.KropExecutionTree.Instruction
{
    /// <summary>
    /// Declaration of an Int variable instruction
    /// </summary>
    class InstructionString : Executable
    {
        Subprogram ParentSubprogram;
        string VarValue;
        string VarName;
        bool Error = false;
        bool inputValue = false;
        string ErrorMsg;

        public InstructionString(Node _nodeIntStatement, Subprogram _parentSubprogram)
        {
            this.ParentSubprogram = _parentSubprogram;

            for (int i = 0; i < _nodeIntStatement.GetChildCount(); i++)
            {
                switch (_nodeIntStatement.GetChildAt(i).GetId())
                {
                    case (int)KropConstants.WORD:
                        Token token = (Token)_nodeIntStatement.GetChildAt(i);
                        VarName = token.GetImage();
                        break;

                    case (int)KropConstants.STRING_VAR_VALUE:

                        switch (_nodeIntStatement.GetChildAt(i).GetChildAt(0).GetId())
                        {
                            case (int)KropConstants.STRING_EXPRESSION:
                                VarValue = AlgorithmicExpression.CalculStringExpression(_nodeIntStatement.GetChildAt(i).GetChildAt(0), _parentSubprogram);
                                break;
                            case (int)KropConstants.INPUT:
                                inputValue = true;
                                break;
                        }
                        break;
                }
            }

            
        }

        public override bool Execute()
        {
            if (!Subprogram.VarExists(VarName, ParentSubprogram))
            {
                if (inputValue)
                    VarValue = Subprogram.PromptInputValue<string>();

                if (VarValue != null)
                    ParentSubprogram.ListVar.Add(new StringVar(VarName, VarValue));
                else
                {
                    Error = true;
                    ErrorMsg = "Variable " + VarName + " ne peut pas être égal à NULL.";
                }
            }
            else
            {
                Error = true;
                ErrorMsg = "Variable " + VarName + " existe déjà.";
            }

            if (!Error)
            {
               
                return true;
            }
            else
            {
                FormControlWindow.TerminalWriteLine("VarError : " + ErrorMsg);
                return false;
            }
        }
    }
}
